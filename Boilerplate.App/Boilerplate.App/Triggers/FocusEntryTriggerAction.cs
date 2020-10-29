using System.Threading.Tasks;
using Xamarin.Forms;

namespace Boilerplate.App.Triggers
{
    public class FocusEntryTriggerAction : TriggerAction<Entry>
    {
        public bool Focused { get; set; }

        protected override async void Invoke(Entry entry)
        {
            await Task.Delay(100);

            if (Focused)
            {
                entry.Focus();
            }
            else
            {
                entry.Unfocus();
            }
        }
    }
}
