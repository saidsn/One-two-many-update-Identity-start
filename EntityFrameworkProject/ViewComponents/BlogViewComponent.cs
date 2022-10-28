﻿using EntityFrameworkProject.Data;
using EntityFrameworkProject.Models;
using EntityFrameworkProject.ViewModels.BlogViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkProject.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            BlogListVM blogList = new BlogListVM
            {
                Blogs = await GetAllBlogs(),
                BlogHeader = await GetBlogHeaders()

            };
            return await Task.FromResult(View(blogList)); 
        }
        private async Task<IEnumerable<Blog>>GetAllBlogs() => await _context.Blogs.Where(m => !m.IsDeleted).ToListAsync();
        private async Task<BlogHeader> GetBlogHeaders() => await _context.BlogHeaders.Where(m => !m.IsDeleted).FirstOrDefaultAsync();
    }
}
