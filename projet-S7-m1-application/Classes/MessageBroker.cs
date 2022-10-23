using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class MessageBroker
    {
        //private IModel clientToCusineChannel;
        private IModel clientToCuisine;
        private IModel cuisineToLivreur;

        
        public MessageBroker() {

            var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
            var connection = factory.CreateConnection();

            this.clientToCuisine = connection.CreateModel();
            this.clientToCuisine.ExchangeDeclare("client-cuisine-topic", "topic");

            this.cuisineToLivreur = connection.CreateModel();
            this.cuisineToLivreur.ExchangeDeclare("cuisine-livreur-topic", "topic");

        }


        public void SendMessageToLivreur(CustomerOrder customerOrder)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customerOrder));
            this.cuisineToLivreur.BasicPublish("cuisine-livreur-topic", "", null, body);
            Console.WriteLine("Message sent to livreur");
        }
        public void SendMessageToCuisine(CustomerOrder customerOrder)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(customerOrder));
            this.clientToCuisine.BasicPublish("client-cuisine-topic", "", null, body);
            Console.WriteLine("Message sent to cuisine");
        }

        public void ListenToCuisineEvent()
        {
            var queueName = this.clientToCuisine.QueueDeclare().QueueName;
            this.clientToCuisine.QueueBind(queue: queueName,
                             exchange: "client-cuisine-topic",
                             routingKey: "");

            var consumer = new EventingBasicConsumer(this.clientToCuisine);
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("Recevice message");
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                CustomerOrder customerOrder = JsonConvert.DeserializeObject<CustomerOrder>(message);
                var routingKey = ea.RoutingKey;
                Console.WriteLine(" [x] Received '{0}':'{1}'",routingKey,message);
                new ToastContentBuilder()
                    .AddText("KITCHEN")
                    .AddText("New order : " + customerOrder.CustomerOrderID + " " + customerOrder.price + "€")
                    .Show();
            };
            this.clientToCuisine.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        }
        public void ListenToLivreurEvent()
        {
            var queueName = this.clientToCuisine.QueueDeclare().QueueName;
            this.cuisineToLivreur.QueueBind(queue: queueName,
                             exchange: "cuisine-livreur-topic",
                             routingKey: "");

            var consumer = new EventingBasicConsumer(this.cuisineToLivreur);
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("Recevice message");
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                CustomerOrder customerOrder = JsonConvert.DeserializeObject<CustomerOrder>(message);
                var routingKey = ea.RoutingKey;
                Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
                new ToastContentBuilder()
                    .AddText("LIVREUR")
                    .AddText("Order : " + customerOrder.CustomerOrderID + " need delivrey ASAP : " + customerOrder.price + "€")
                    .Show();
            };
            this.cuisineToLivreur.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        }

    }
}
