﻿using System;

namespace MiCakeDemoApplication.Dto
{
    public class ChangeBookAuthorDto
    {
        public Guid BookID { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
    }
}
