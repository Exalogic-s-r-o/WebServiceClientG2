using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.UI.ViewModels
{
    public partial class StockViewModel : BaseViewModel
    {

        public StockViewModel(Base.AppEngine appEngine,
                              IPopupService popupService) : base(appEngine, popupService)
        {

        }

        #region METHODS

        /// <summary>
        /// Načítanie zoznamu stromových štruktúr (kategórií).
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task TreeCategoryListLoad()
        {
            if (this.IsRunning == true)
            {
                return;
            }

            try
            {
                this.IsRunning = true;

                var result = await this._AppEngine.WebServiceClient.Stock.TreeCategoriesList(getImages: false);
                if (result.result == false)
                {
                    // Chyba
                    WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"Chyba pri volaní metódy 'tree category list'. '{result.description}'."));
                    return;
                }

                var categoriesPath = ConvertCategoryPath(result.data);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Tree category list");
                foreach (var item in result.data.Items)
                {
                    sb.AppendLine($" Name: {item.Name}");
                }
                WeakReferenceMessenger.Default.Send(new WebServiceClientG2.Messages.AddTextMessage($"{sb}"));
            }
            catch
            {
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        public static List<string> ConvertCategoryPath(Exa.OBERON.ServicesGen2.Client.Models.Stock.StockCardsTreeCategoriesData stockCardsTreeCategoriesData)
        {
            try
            {
                List<string> absolutePathCategories = new List<string>();

                foreach (var e_Category in stockCardsTreeCategoriesData.Items)
                {
                    if (e_Category.IDNum_Parent == 0)
                    {
                        // Je root kategória, nepridávať nič.
                        absolutePathCategories.Add($"\\{e_Category.Name}\\");
                    }
                    else
                    {
                        // Je niečí child, nájsť rodiča a zostaviť absolútnu cestu.
                        string absolutePath = $"\\{e_Category.Name}\\";
                        long parentId = e_Category.IDNum_Parent;
                        while (parentId != 0)
                        {
                            var parentCategory = stockCardsTreeCategoriesData.Items.Find(c => c.IDNum == parentId);
                            if (parentCategory != null)
                            {
                                absolutePath = $"\\{parentCategory.Name}{absolutePath}";
                                parentId = parentCategory.IDNum_Parent;
                            }
                            else
                            {
                                // Rodič sa nenašiel, prerušiť slučku.
                                break;
                            }
                        }

                        absolutePathCategories.Add(absolutePath);
                    }
                }

                return absolutePathCategories;

            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        #endregion
    }
}
