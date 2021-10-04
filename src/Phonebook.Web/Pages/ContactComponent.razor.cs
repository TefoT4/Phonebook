using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Phonebook.Domains;

namespace Phonebook.Web.Pages
{
    public partial class ContactComponent
    {
        [Inject]
        private HttpClient Http { get; set; }

        [Inject] 
        private IToastService ToastService { get; set; }

        [CascadingParameter] 
        BlazoredModalInstance ModalInstance { get; set; }

        [Parameter] 
        public bool IsUpdateCommand { get; set; }

        [Parameter]
        public Contact Contact { get; set; }

        private Contact ContactCopy { get; set; }

        protected override void OnInitialized()
        {
            ContactCopy = new Contact()
            {
                Id = Contact.Id,
                City = Contact.City,
                Created = Contact.Created,
                Deleted = Contact.Deleted,
                Email = Contact.Email,
                Name = Contact.Name,
                PhoneNumber = Contact.PhoneNumber,
                PictureUrl = Contact.PictureUrl,
                Updated = Contact.Updated
            };
        }

        private async void SaveContact(MouseEventArgs obj)
        {
            HttpResponseMessage httpResponseMessage;

            if (IsUpdateCommand)
            {
                ContactCopy.Id = Contact.Id;
                ContactCopy.Created = Contact.Created;
                ContactCopy.Deleted = Contact.Deleted;
                ContactCopy.Updated = Contact.Updated;
                httpResponseMessage = await Http.PutAsJsonAsync($"Phonebook/{ContactCopy.Id}", ContactCopy);
            }
            else
                httpResponseMessage = await Http.PostAsJsonAsync("Phonebook", ContactCopy);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadFromJsonAsync<Contact>();

                if (content != null)
                {
                    ToastService.ShowSuccess($"{Contact.Name} Saved.");
                }
                
                await ModalInstance.CloseAsync(ModalResult.Ok(content));
            }
            else
            {
                ToastService.ShowError("Error Adding Contact.");
            }
        }
    }
}
