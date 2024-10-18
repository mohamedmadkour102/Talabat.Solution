﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.DTO_s;
using Talabat.Errors;

namespace Talabat.Controllers
{
   
    public class BasketController :BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")] // Get ==> api/Basket/id
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            // Mapping from dto to model 
            var MappedBasket = _mapper.Map<CustomerBasketDto , CustomerBasket>(basket);
            var Createorupdatebasket = await _basketRepository.UpdateBasketAsync(MappedBasket);
            if (Createorupdatebasket is null) return BadRequest(new ApiResponse(400));
            return Ok(Createorupdatebasket);
        }

        [HttpDelete]

        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
