﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> Purchase([FromBody] PurchaseRequestModel purchaseRequest)
        {

            await _userService.Purchase(purchaseRequest);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        //in mvc authorize attribute will look for Cookie
        //in API, it will look for JWT in the header
        [Route("PurchasedMovies/{userId:int}")]
        public async Task<IActionResult> PurchasedMovies(int userId)
        {
            
            var movies=await _userService.PurchasedMovies(userId);
            return Ok(movies);
        }

        [HttpGet]
        [Route("FavoritedMovies/{userId:int}")]
        public async Task<IActionResult> FavoritedMovies(int userId)
        {

            var movies = await _userService.FavoritedMovies(userId);
            return Ok(movies);
        }
        [HttpGet]
        [Route("{userId:int}/movie/{movieId:int}/IsPurchased")]
        public async Task<IActionResult> IsPurchased(int userId, int movieId)
        {
            return Ok(await _userService.IsMoviePurchased(userId, movieId));
        }

        [HttpPost]
        [Route("Review")]
        public async Task<IActionResult> Review([FromBody] Review review)
        {

            await _userService.SaveReview(review);
            return Ok();
        }

        [HttpGet]
        [Route("{userId:int}/ReviewListbyUser")]
        public async Task<IActionResult> ReviewListbyUser(int userId)
        {
            var reviews = await _userService.ReviewListbyUser(userId);
            var reviewDTOs= _mapper.Map<List<ReviewDTO>>(reviews);
            return Ok(reviewDTOs);
        }

        [HttpPost]
        [Route("Favorite")]
        public async Task<IActionResult> Favorite([FromBody] FavoriteRequestModel favoriteRequestModel)
        {

            await _userService.Favorite(favoriteRequestModel);
            return Ok();
        }

        [HttpGet]
        [Route("{userId:int}/movie/{movieId:int}/IsFavorited")]
        public async Task<IActionResult> IsFavorited(int userId, int movieId)
        {
            return Ok(await _userService.IsFavorited(userId, movieId));
        }

        [HttpPost]
        [Route("RemoveFavorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRequestModel favoriteRequestModel)
        {

            await _userService.RemoveFavorite(favoriteRequestModel);
            return Ok();
        }
    }
}
