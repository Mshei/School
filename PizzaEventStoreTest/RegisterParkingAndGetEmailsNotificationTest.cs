using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Win32;
using Moq;
//using Mocks;
using Newtonsoft.Json;
using ParkingLotCase;
using Xunit;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace ParkingLotCaseTest
{
    public class RegisterParkingAndGetEmailsNotificationTest : IDisposable
    {
        private static int mocksPort = 5050;
        private readonly MocksHost serviceMock;
        private readonly IHost loyaltyProgramHost;
        private readonly HttpClient sut;
        IParkingLotServices ParkingLotServices = null;

        private readonly ParkingLotServices _sut;
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly ParkingLotStore _parkingLotStore = new ParkingLotStore();

        public RegisterParkingAndGetEmailsNotificationTest()
        {
            _sut = new ParkingLotServices((IHttpClientFactory)_httpClientFactory);
        }

        [Fact]
        public void checkParking()
        {
            // Arrange 
            Random rnd = new Random();
            ParkingSpaces ParkingSpaces = new ("CR30188");

            ParkingSpaces.Email = "ms@axdata.com";
            ParkingSpaces.ParkingFloor = rnd.Next(1, 10);
            ParkingSpaces.ParkingSpace = "P" + rnd.Next(1, 10);
            Boolean parked;
            // Act
            parked = _parkingLotStore.checkParking(ParkingSpaces);
            // Assert
            Assert.True(parked, "Car is parked");
            Assert.False(parked, "Car is not parked");
        }

        [Fact]
        public async void emailSentTest()
        {
            // Arrange 
            String _licensePlate = "CR30188"; 
            String _email = "ms@axdata.com";
            Boolean emailSent;
            // Act
            emailSent = await _sut.SendEmailAsync(_licensePlate, _email);
            // Assert
            Assert.True(emailSent, "Mail has been sent");

        }

        [Fact]
        public async Task Scenario()
        {
            await RegisterNewParking();
            //await RunConsumer();
        }
        private async Task RegisterNewParking()
        {
            /*var actual = await this.sut.PostAsync(
            "/SetParkingLot",
            new StringContent(
            JsonSerializer.Serialize(
            new ParkingSpaces("CR30188"),
            0, new ParkingCarDescription())),
            Encoding.UTF8,
            "application/json");*/

            Random rnd = new Random();

            ParkingSpaces registerParkingSpace = new ParkingSpaces("CR30188");
            registerParkingSpace.PhoneNumber = "12345678";
            registerParkingSpace.Email = "ms@axdata.com";
            registerParkingSpace.DateTime = DateTime.Now;
            registerParkingSpace.ParkingFloor = rnd.Next(1, 10);
            registerParkingSpace.ParkingSpace = "P" + rnd.Next(1, 10);

            registerParkingSpace = await ParkingLotServices.GetDescriptionAsync(registerParkingSpace, "CR30188");

            //Assert.IsType<ParkingSpaces>(registerParkingSpace);
            Assert.True(registerParkingSpace.IsParked, "Vehicle is parked");

        }


        /*private Task RunConsumer() =>
        ParkingLotServices.ParkingLotController(
        0,
        100,
        $"http://localhost:{mocksPort}/SetParkingLot",
        $"http://localhost:{mocksPort}"
        );*/
        public void Dispose()
        {
            this.serviceMock?.Dispose();
            this.sut?.Dispose();
            this.loyaltyProgramHost?.Dispose();
        }
    }

}
