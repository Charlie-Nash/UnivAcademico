using Microsoft.AspNetCore.Connections;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using UnivAcademico.Application.Events;

namespace UnivAcademico.Api.Helpers
{
    public class RabbitMqPublisher
    {
        public void PublicarEventoMatriculaRegistrada(MatriculaRegistradaEvent evento)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqps://cbjmdvyc:dIL2jaIJ9l2f7GZJgxYBFrQKo9owmFvW@campbell.lmq.cloudamqp.com/cbjmdvyc")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "matricula-registrada",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(evento);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: "matricula-registrada",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
