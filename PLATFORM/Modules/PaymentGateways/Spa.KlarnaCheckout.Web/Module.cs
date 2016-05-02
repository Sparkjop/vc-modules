using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Spa.KlarnaCheckout.Web.Managers;
using Spa.KlarnaCheckout.Web.Resources;
using VirtoCommerce.Domain.Payment.Services;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Settings;

namespace Spa.KlarnaCheckout.Web
{
    public class Module : ModuleBase
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        #region IModule Members

        public override void Initialize()
        {
            var settingsManager = ServiceLocator.Current.GetInstance<ISettingsManager>();

            Func<SpaKlarnaCheckoutPaymentMethod> spaKlarnaCheckoutPaymentMethodFactory = () => new SpaKlarnaCheckoutPaymentMethod
            {
                Name = SpaKlarnaCheckoutResource.PaymentMethodName,
                Description = SpaKlarnaCheckoutResource.PaymentMethodDescription,
                LogoUrl = SpaKlarnaCheckoutResource.PaymentMethodLogoUrl,
                Settings = settingsManager.GetModuleSettings("Spa.KlarnaCheckout")
            };

            var paymentMethodsService = _container.Resolve<IPaymentMethodsService>();
            paymentMethodsService.RegisterPaymentMethod(spaKlarnaCheckoutPaymentMethodFactory);
        }

        #endregion
    }
}