namespace SurveyMaker.Test
{
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Web.Infrastructure.Mapping;

    public class Tests
    {
        private static bool testInitialized = false;

        public static void Initialize()
        {
            if (!testInitialized)
            {
                Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
                testInitialized = true;
            }
        }

        public static SurveyMakerDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<SurveyMakerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SurveyMakerDbContext(dbOptions);
        }
    }
}
