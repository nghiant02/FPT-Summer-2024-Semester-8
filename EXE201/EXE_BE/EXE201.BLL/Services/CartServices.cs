using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.CartDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Services
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRentalOrderDetailRepository _rentalOrderDetailRepository;
        private readonly IMapper _mapper;

        public CartServices(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository,
            IRentalOrderDetailRepository rentalOrderDetailRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _rentalOrderDetailRepository = rentalOrderDetailRepository;
            _mapper = mapper;
        }

        public async Task<Cart> AddNewCart(AddNewCartDTO cartDTO)
        {
            var cartEntity = _mapper.Map<Cart>(cartDTO);

            await _cartRepository.AddNewCart(cartEntity);

            return cartEntity;
        }

        public async Task<bool> DeleteCart(int id)
        {
            return await _cartRepository.DeleteCartById(id);
        }

        public async Task<IEnumerable<ViewCartDto>> GetAllCarts()
        {
            var carts = await _cartRepository.GetAll();
            var cartDto = new List<ViewCartDto>();
            foreach (var cart in carts)
            {
                var product = await _productRepository.GetById(cart.Product.ProductId);
                var rentalOrderDetail = cart.Product.RentalOrderDetails.FirstOrDefault();
                var rentalOrder = rentalOrderDetail != null ? await _rentalOrderDetailRepository.GetRentalOrderDetail(rentalOrderDetail.OrderDetailsId) : null;

                var viewCart = new ViewCartDto
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    ProductId = cart.ProductId,
                    ProductTitle = cart.Product.ProductTitle,
                    Quantity = cart.Quantity,
                    ProductPrice = cart.Product.ProductPrice,
                    ProductImageUrl = product.ProductImage.ToList(),
                    RentalStart = rentalOrder?.RentalStart,
                    RentalEnd = rentalOrder?.RentalEnd
                };
                cartDto.Add(viewCart);
            }

            return cartDto;
        }

        public async Task<IEnumerable<ViewCartDto>> GetCartById(int userId)
        {
            var carts = await _cartRepository.GetCartsByUserId(userId);
            var cartDtoList = new List<ViewCartDto>();

            foreach (var cart in carts)
            {
                var product = await _productRepository.GetById(cart.Product.ProductId);
                var rentalOrderDetail = cart.Product.RentalOrderDetails.FirstOrDefault();
                if (rentalOrderDetail != null)
                {
                    var rentalOrder = await _rentalOrderDetailRepository.GetRentalOrderDetail(rentalOrderDetail.OrderDetailsId);
                    var cartDto = new ViewCartDto
                    {
                        CartId = cart.CartId,
                        UserId = cart.UserId,
                        ProductId = cart.ProductId,
                        ProductTitle = cart.Product.ProductTitle,
                        Quantity = cart.Quantity,
                        ProductPrice = cart.Product.ProductPrice,
                        ProductImageUrl = product.ProductImage.ToList(),
                        RentalStart = rentalOrder.RentalStart,
                        RentalEnd = rentalOrder.RentalEnd
                    };

                    cartDtoList.Add(cartDto);
                }
            }

            return cartDtoList;
        }

        public async Task<ViewCartDto> UpdateCartByUserId(int userId, UpdateCartDto cart)
        {
            var checkCart = await _cartRepository.GetCartById(cart.CartId);
            if (checkCart == null)
            {
                throw new Exception("CartId does not exist!");
            }

            if (checkCart.UserId != userId)
            {
                throw new Exception("UserId does not match the Cart's UserId!");
            }

            var rentalOrder = await _rentalOrderDetailRepository.GetRentalOrderDetail(checkCart.Product.RentalOrderDetails.First().OrderDetailsId);
            if (rentalOrder == null)
            {
                throw new Exception("RentalOrderDetails do not exist!");
            }

            var product = await _productRepository.GetById(checkCart.Product.ProductId);
            if (product == null)
            {
                throw new Exception("Product does not exist!");
            }

            checkCart.Quantity = cart.Quantity;
            rentalOrder.RentalStart = cart.RentalStart;
            rentalOrder.RentalEnd = cart.RentalEnd;

            var updatedCart = await _cartRepository.UpdateCart(checkCart);
            var updatedRentalOrder = await _rentalOrderDetailRepository.UpdateRentalDetail(rentalOrder);

            var productPrice = product.ProductPrice ?? 0;
            var totalPrice = productPrice * updatedCart.Quantity;
            var viewCartDto = new ViewCartDto
            {
                CartId = updatedCart.CartId,
                UserId = updatedCart.UserId,
                ProductId = updatedCart.ProductId,
                ProductTitle = product.ProductTitle,
                ProductPrice = totalPrice,
                ProductImageUrl = product.ProductImage.ToList(),
                RentalStart = updatedRentalOrder.RentalStart,
                RentalEnd = updatedRentalOrder.RentalEnd,
                Quantity = updatedCart.Quantity
            };
            return viewCartDto;
        }
    }
}