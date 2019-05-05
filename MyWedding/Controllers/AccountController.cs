﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWedding.Services;
using MyWedding.Models;

namespace MyWedding.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _messageService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailService messageService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._messageService = messageService;
        }

        [Route("~/Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("~/Register")]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {

                var newUser = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Username
                };

                var userCreationResult = await _userManager.CreateAsync(newUser, model.Password);
                if (!userCreationResult.Succeeded)
                {
                    foreach (var error in userCreationResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View();
                }

                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var tokenVerificationUrl = Url.Action("VerifyEmail", "Admin", new { id = newUser.Id, token = emailConfirmationToken }, Request.Scheme);

                await _messageService.SendEmail(model.Username, "Verify your email", $"Click <a href=\"{tokenVerificationUrl}\">here</a> to verify your email");

                return Content("Check your email for a verification link");
            }
            return View(model);

        }


        public async Task<IActionResult> VerifyEmail(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            var emailConfirmationResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!emailConfirmationResult.Succeeded)
                return Content(emailConfirmationResult.Errors.Select(error => error.Description).Aggregate((allErrors, error) => allErrors += ", " + error));

            return Content("Email confirmed, you can now log in");
        }

        [Route("~/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("~/Login")]
        public async Task<IActionResult> Login(LoginUser model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Confirm your email first");
                return View();
            }

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: model.rememberme, lockoutOnFailure: false);
            if (!passwordSignInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();
            }

            return Redirect("~/Guest");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Content("Check your email for a password reset link");

            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetUrl = Url.Action("ResetPassword", "Admin", new { id = user.Id, token = passwordResetToken }, Request.Scheme);

            await _messageService.SendEmail(email, "Password reset", $"Click <a href=\"" + passwordResetUrl + "\">here</a> to reset your password");

            return Content("Check your email for a password reset link");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, string token, string password, string repassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            if (password != repassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match");
                return View();
            }

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPasswordResult.Succeeded)
            {
                foreach (var error in resetPasswordResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View();
            }

            return Content("Password updated");
        }
    }
}