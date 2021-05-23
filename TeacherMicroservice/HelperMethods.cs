﻿using Domain.VMs;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskAPI
{
    public static class HelperMethods
    {
        public static string getException(Exception exception) {
            string formattedException=exception.Message;
            if (exception.InnerException != null)
            {
                formattedException+= getException(exception.InnerException);
            }
            return formattedException;
        }
        public static void Producer(byte[] bytesObject) {
            string jsonString = Encoding.UTF8.GetString(bytesObject);
            var josnObject = JsonConvert.DeserializeObject<TeacherVM>(jsonString);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queueName=josnObject.GetType().Name;
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //string message = josnString;
                var body = bytesObject;//Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
