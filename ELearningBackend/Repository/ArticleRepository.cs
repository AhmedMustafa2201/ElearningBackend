﻿using ELearningBackend.Models;
using ELearningBackend.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearningBackend.Repository
{
    public class ArticleRepository:Repository<Article>,IArticleRepository
    {
        public ArticleRepository(ApplicationDBContext context):base(context)
        {

        }

        public async Task<Article> GetArticleByIdAsync(int ArticleId)
        {
            return await context.Articles.Where(a=>a.Id==ArticleId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Article>> GetRelatedAsync(int ArticleId)
        {
            var article = await context.Articles.FindAsync(ArticleId);
            var topics = await context.Topics.Where(t => t.Articles.Contains(article)).ToListAsync();

            List<Article> articles = new List<Article>();

            foreach (var topic in topics)
            {
                if (articles.Count >= 5)
                    break;
                var range = await context.Articles.Where(q => q.Topics.Contains(topic)).ToListAsync();
                articles.AddRange(range.FindAll(x => {
                    return !articles.Contains(x) && x.Id != ArticleId;
                }));

            }

            return articles;
        }


<<<<<<< HEAD
        public async Task<IEnumerable<Article>> GetSomeArticleAsync()
        {
            return await context.Articles.Take(3).ToListAsync();
        }
=======
>>>>>>> 4510c06e4c2f298e093d5fdac4e668d1bd1a53cd
    }
}
