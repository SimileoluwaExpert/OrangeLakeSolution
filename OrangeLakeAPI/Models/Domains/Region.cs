﻿namespace OrangeLakeAPI.Models.Domains
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string code {  get; set; } 

        public string? RegionImageUrl { get; set; }
    }
}
