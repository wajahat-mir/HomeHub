using Cassandra;
using Cassandra.Mapping;
using HomeHub.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace HomeHub.Services
{

    public interface IIoTMessageService
    {
        bool ValidateServerCertificate(object sender,
           X509Certificate certificate,
           X509Chain chain,
           SslPolicyErrors sslPolicyErrors);
        IEnumerable<IoTMessage> QueryByDeviceId(string DeviceId);
    }

    public class IoTMessageService : IIoTMessageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private Cluster cluster;

        public IoTMessageService(IConfiguration configuration, ILogger<IoTMessageService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
            options.SetHostNameResolver((ipAddress) => _configuration["DatabaseSettings:ContactPoint"]);

            cluster = Cluster.Builder().WithCredentials(_configuration["DatabaseSettings:UserName"], _configuration["DatabaseSettings:Password"])
                .WithPort(Convert.ToInt32(_configuration["DatabaseSettings:Port"])).AddContactPoint(_configuration["DatabaseSettings:ContactPoint"]).WithSSL(options).Build();
        }

        public IEnumerable<IoTMessage> QueryByDeviceId(string DeviceId)
        {
            string Query = "SELECT DeviceId, TimeStamp, Status, message FROM Messages WHERE DeviceId = '" + DeviceId + "'";

            Cassandra.ISession session = cluster.Connect(_configuration["DatabaseSettings:KeySpace"]);
            IMapper mapper = new Mapper(session);
            return mapper.Fetch<IoTMessage>(Query);
        }

        public bool ValidateServerCertificate(
           object sender,
           X509Certificate certificate,
           X509Chain chain,
           SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            _logger.LogError("Certificate error: {0}", sslPolicyErrors);
            return false;
        }
    }
}
