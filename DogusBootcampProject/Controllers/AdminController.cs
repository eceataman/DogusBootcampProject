﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogusBootcampProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

    }
}
