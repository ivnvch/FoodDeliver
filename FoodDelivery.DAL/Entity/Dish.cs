﻿using FoodDelivery.Models.Enum;

namespace FoodDelivery.DAL.Entity
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public short Amount { get; set; }
        public Category Category { get; set; }

    }
}