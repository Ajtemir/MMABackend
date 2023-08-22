namespace MMABackend.Enums.Common
{
    /// <summary>
    /// Тип магазина
    /// </summary>
    public enum ShopType
    {
        /// <summary>
        /// Онлайн
        /// Будет null в координатах
        /// </summary>
        Online = 1,
        /// <summary>
        /// Типа бутик
        /// будет локация в городе
        /// </summary>
        Fixed = 2,
        /// <summary>
        /// Стихийная торговля
        /// координаты в городе
        /// </summary>
        Free = 3,
        /// <summary>
        /// Внутри базара либо торгового центра
        /// подробно расписать их расположение
        /// </summary>
        Market = 4,
    }
}