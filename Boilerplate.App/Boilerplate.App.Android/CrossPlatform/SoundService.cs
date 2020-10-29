using Android.Media;
using Boilerplate.App.CrossPlatform;
using Boilerplate.App.Droid.Dependencies;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SoundService))]
namespace Boilerplate.App.Droid.Dependencies
{
    public class SoundService : ISoundService
    {
        public async void Success()
        {
            ToneGenerator tg = new ToneGenerator(Stream.Music, 100);
            tg.StartTone(Tone.CdmaConfirm);
            await Task.Delay(500);
            tg.Release();
        }

        public async void Warning()
        {
            ToneGenerator tg = new ToneGenerator(Stream.Music, 100);
            tg.StartTone(Tone.PropAck);
            await Task.Delay(500);
            tg.Release();
        }

        public async void Error()
        {
            ToneGenerator tg = new ToneGenerator(Stream.Music, 100);
            tg.StartTone(Tone.CdmaCalldropLite);
            await Task.Delay(500);
            tg.Release();
        }
    }
}