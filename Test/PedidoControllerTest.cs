using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Repository;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Test
{
    [TestClass]
    public class PedidoControllerTest
    {
        private ApplicationDbContext _context;
        private PedidoController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=TOMASMONDIGLIO\\TOMAS;Database=PedidosDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;
            _context = new ApplicationDbContext(options);

            _controller = new PedidoController(
                new PedidoRepository(_context),
                new ClienteRepository(_context),
                new PedidoItemRepository(_context)
            );
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Limpia los datos generados por las pruebas
            _context.Pedidos.RemoveRange(_context.Pedidos.Where(p => p.ContactName.Contains("TEST_")));
            _context.Clientes.RemoveRange(_context.Clientes.Where(c => c.RazonSocial.Contains("TEST_")));
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task Given_ARequestWithPastDate_When_Create_Then_ShouldReturnBadRequest()
        {
            // Arrange
            var pedido = new Pedido
            {
                FechaPedido = DateTime.Today.AddDays(-1),
                FechaEntrega = DateTime.Today,
                ContactName = "TEST_Cliente",
                IdCliente = 1
            };

            // Act
            var result = await _controller.Create(pedido);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("La fecha del pedido no puede ser anterior a hoy", badRequestResult.Value);
        }

        [TestMethod]
        public async Task Given_AnInvalidClientId_When_Create_Then_ShouldReturnBadRequest()
        {
            // Arrange
            var pedido = new Pedido
            {
                FechaPedido = DateTime.Today,
                FechaEntrega = DateTime.Today.AddDays(1),
                ContactName = "TEST_Cliente",
                IdCliente = 999
            };

            // Act
            var result = await _controller.Create(pedido);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("El cliente especificado no existe en el sistema", badRequestResult.Value);
        }

        [TestMethod]
        public async Task Given_AValidRequest_When_Create_Then_ShouldReturnCreated()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cuit = "TEST_20123456789",
                RazonSocial = "TEST_Cliente Válido",
                Direccion = "Test Dir",
                Localidad = "Test Loc"
            };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            var pedido = new Pedido
            {
                FechaPedido = DateTime.Today,
                FechaEntrega = DateTime.Today.AddDays(1),
                ContactName = "TEST_Cliente",
                IdCliente = cliente.IdCliente
            };

            // Act
            var result = await _controller.Create(pedido);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }
    }
}