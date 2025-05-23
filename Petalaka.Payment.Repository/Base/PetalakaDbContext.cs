﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petalaka.Payment.Repository.Entities;

namespace Petalaka.Payment.Repository.Base
{
    public class PetalakaDbContext : DbContext
    {
        public PetalakaDbContext(DbContextOptions<PetalakaDbContext> options) : base(options)
        {
        }
        public DbSet<PaymentGateway> PaymentGateways => Set<PaymentGateway>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<CartItemAdditionalDetail> CartItemAdditionalDetails => Set<CartItemAdditionalDetail>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();
        public DbSet<OrderDetailAdditionalDetail> OrderDetailAdditionalDetails => Set<OrderDetailAdditionalDetail>();
    }
}
