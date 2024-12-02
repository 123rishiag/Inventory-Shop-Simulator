using ServiceLocator.Event;
using ServiceLocator.Item;
using ServiceLocator.UI;

namespace ServiceLocator.Shop
{
    public class ShopView
    {
        private ShopController shopController;

        public ShopView(ShopController _shopController)
        {
            shopController = _shopController;
        }

        public void UpdateUI(EventService _eventService, UIService _uiService)
        {
            _eventService.OnShowItemEvent.Invoke(shopController.GetModel().UISection,
                shopController.GetModel().SelectedItemType);

            // Update UI texts based on filtered items
            _uiService.UpdateShopEmptyText(shopController.GetFilteredItemsCount() == 0);
            _uiService.UpdateShopItemTypeText(shopController.GetModel().SelectedItemType.ToString());
            _uiService.UpdateShopItemCount(shopController.GetFilteredItemsCount());

            _uiService.HideItemPanel();
        }
    }
}