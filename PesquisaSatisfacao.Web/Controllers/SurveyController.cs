using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PesquisaSatisfacao.Core.Application;
using Microsoft.AspNetCore.Identity;
using PesquisaSatisfacao.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using PesquisaSatisfacao.Web.Models.Surveys;

namespace PesquisaSatisfacao.Web.Controllers
{
    [Authorize]
    public class SurveyController : BaseController
    {
        readonly SurveyAppService service;
        public SurveyController(SurveyAppService service, SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> userManager) : base(_signInManager, userManager)
        {
            this.service = service;
        }

        public async Task<IActionResult> List(SurveyListViewModel model)
        {
            var userId = GetUserId();
            model.Surveys = await service.GetBy(userId, model.BeginDate, model.EndDate);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SurveyCreateViewModel model)
        {
            try
            {
                var userId = GetUserId();
                await service.Add(model.ToDomain(userId));
                return Json(new { Success = true, Message = "Pesquisa adicionada com sucesso"});
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Desactive(int id)
        {
            try
            {
                await service.Desactive(id);
                return Json(new { Success = true, Message = "Pesquisa desativada com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message });
            }
        }

        public async Task<IActionResult> QuestionList(int surveyId)
        {
            var model = new QuestionListViewModel();
            model.Questions = await service.GetQuestionsBy(surveyId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateQuestion(int surveyId)
        {
            var model = new QuestionCreateViewModel(surveyId);
            var userId = GetUserId();
            model.Categorys = await service.GetCategorys(userId);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(QuestionCreateViewModel model)
        {
            try
            {
                await service.AddQuestion(model.ToDomain());
                return Json(new { Success = true, Message = "Questao adicionada com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message });
            }
        }

        public async Task<IActionResult> CategoryList()
        {
            var model = new CategoryListViewModel();
            var userId = GetUserId();
            model.Categorys = await service.GetCategorys(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateViewModel model)
        {
            try
            {
                var userId = GetUserId();
                await service.AddCategory(model.ToDomain(userId));
                return Json(new { Success = true, Message = "Categoria adicionada com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message });
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> AnswerQuestion(string code)
        {
            var model = new AnswerQuestionViewModel();
            model.Questions = await service.GetQuestionsBy(code);
            model.Answers = await service.GetAnswers();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateQuestionAnswer(AnswerQuestionViewModel model)
        {
            try
            {
                await service.AddQuestionAnswer(model.QuestionAnswers);
                return Json(new { Success = true, Message = "Pesquisa respondida com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message });
            }
        }

        public async Task<IActionResult> ReportChart()
        {
            var model = new ReportChartViewModel();
            var userId = GetUserId();
            model.Surveys = await service.GetBy(userId, null, null);
            model.Answers = await service.GetAnswers();
            return View(model);
        }

        public async Task<IActionResult> _ReportChart(int surveyId, int answerId)
        {
            var model = new ReportChartViewModel();
            model.Items = await service.SurveyChartBy(surveyId, answerId);
            return PartialView(model);
        }

        public async Task<IActionResult> ReportMain()
        {
            var model = new ReportMainViewModel();
            var userId = GetUserId();
            model.Categorys = await service.GetCategorys(userId);
            model.Surveys = await service.GetBy(userId, null, null);
            return View(model);
        }

        public async Task<IActionResult> _ReportMain(int surveyId, int categoryId)
        {
            var model = new ReportMainViewModel();
            model.Item = await service.ReportMain(surveyId, categoryId);
            return PartialView(model);
        }
    }
}