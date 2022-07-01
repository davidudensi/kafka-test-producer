using Confluent.Kafka;
using KafkaTest1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KafkaTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        private ProducerConfig _producerConfig;
        public KafkaController(ProducerConfig producerConfig)
        {
            _producerConfig = producerConfig;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(string topic, [FromBody]Employee employee)
        {
            string serializedData = JsonSerializer.Serialize(employee);
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedData });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }
}
