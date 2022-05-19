﻿using DiplomStore.Domain.Entity;

namespace DiplomStore.ViewsModels.Tovar
{
    public class TovarViewModel
    {
        public int TovarsId { get; set; }

        public string name { get; set; }

        public decimal price { get; set; }

        public bool isValids { get; set; }

        public int? hight { get; set; }

        public int? width { get; set; }
        public int count { get; set; }

        public string? imgPath { get; set; }

        public string? materials { get; set; }

        public string? filler { get; set; }

        public string nameCategory { get; set; }
        public int CategoryId { get; set; }
        public Categories? category { get; set; }

        public string nameTitle { get; set; }

        public int TitleId { get; set; }
        public Titles? titles { get; set; }

    }
}
