using PostomatIntegration.BL.CustomExceptions;
using PostomatIntegration.BL.Models.WebModels;
using PostomatIntegration.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AttributeExtentions;

namespace PostomatIntegration.BL.Servicies
{
	[RegisterService]
	public class ValidationService
	{
		#region validation rules
		public void OrderContentCountRule(OrderRequest order)
		{
			if (order.Content.Count > 10)
				throw new ValidationException();
		}
		/// <summary>
		/// +7XXX-XXX-XX-XX
		/// </summary>
		/// <param name="phone"></param>
		/// <returns></returns>
		public bool PhoneFormatRule(string phone)
		{
			if (string.IsNullOrEmpty(phone))
				return false;

			if (phone.Length != 15)
				return false;

			if (phone[0] != '+' || phone[1] != '7')
				return false;

			var subnum = phone.Substring(2, phone.Length - 2);
			int temp = 0;
			foreach (var numberPart in subnum.Split('-'))
				if (int.TryParse(numberPart, out temp) == false)
					return false;

			return true;
		}
		/// <summary>
		/// XXXX-XXX
		/// </summary>
		/// <param name="postomatNumber"></param>
		/// <returns></returns>
		public bool PostomatFormatRule(string postomatNumber)
		{
			if (string.IsNullOrEmpty(postomatNumber))
				return false;

			if (postomatNumber.Length != 8)
				return false;

			int temp = 0;
			foreach (var partOfNumber in postomatNumber.Split('-'))
				if (int.TryParse(partOfNumber, out temp) == false)
					return false;

			return true;
		}
		public void CreateValidationRules(OrderRequest order)
		{
			if (order == null)
				throw new ObjectNotFoundException();

			OrderContentCountRule(order);

			if (PhoneFormatRule(order.CustomerNumber) == false)
				throw new ValidationException();

			if (PostomatFormatRule(order.Postomat) == false)
				throw new ValidationException();

		}
		public void ChangeValidationRules(Order existingOrder, OrderRequest changedOrder)
		{
			if (existingOrder == null || changedOrder == null)
				throw new ObjectNotFoundException();

			var validationErrors = new List<string>();

			if (existingOrder.Status != changedOrder.OrderStatus)
				throw new ValidationException();

			if (existingOrder.Postomat.Number != changedOrder.Postomat)
				throw new ValidationException();

			OrderContentCountRule(changedOrder);

			if (PhoneFormatRule(changedOrder.CustomerNumber) == false)
				throw new ValidationException();

			if (PostomatFormatRule(changedOrder.Postomat) == false)
				throw new ValidationException();
		}
		#endregion validation rules
	}
}
