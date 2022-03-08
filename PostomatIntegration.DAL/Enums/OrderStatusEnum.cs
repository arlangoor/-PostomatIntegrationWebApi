using System;
using System.Collections.Generic;
using System.Text;

namespace PostomatIntegration.DAL.Enums
{
	public enum OrderStatusEnum
	{
		/// <summary>
		/// Зарегистрирован
		/// </summary>
		Registered = 1,
		/// <summary>
		/// Принят на складе
		/// </summary>
		Accepted_in_stock = 2,
		/// <summary>
		/// Выдан курьеру
		/// </summary>
		Issued_to_the_courier = 3,
		/// <summary>
		/// Доставлен в постамат
		/// </summary>
		Delivered_to_post_office = 4,
		/// <summary>
		/// Доставлен получателю
		/// </summary>
		Delivered_to_recipient = 5,
		/// <summary>
		/// Отменен
		/// </summary>
		Canceled = 6
	}
}
