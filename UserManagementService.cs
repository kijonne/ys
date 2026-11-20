using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using UserService2.Kafka;
using UserService2.Models;

namespace UserService2.Services
{
    public class UserManagementService
    {
        private readonly ConcurrentDictionary<int, User> _users = new();
        private readonly KafkaProducer _producer;

        public UserManagementService(KafkaProducer producer)
        {
            _producer = producer;
        }

        public async Task RegisterUser(User user)
        {
            if (!_users.TryAdd(user.Id, user))
                throw new InvalidOperationException("Пользователь уже существует");

            await _producer.RegisterUser(user);
        }

        public bool UpdateUser(User user)
        {
            if (!_users.ContainsKey(user.Id))
                return false;

            _users[user.Id] = user;
            return true;
        }

        public User? GetUser(int id)
        {
            _users.TryGetValue(id, out var user);
            return user;
        }

        public List<User> GetAllUsers()
        {
            return _users.Values.ToList();
        }
    }
}
