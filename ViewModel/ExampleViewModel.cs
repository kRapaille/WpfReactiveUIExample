using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfReactiveUIExample.ViewModel
{
    public class ExampleViewModel : ReactiveObject
    {
        int _amountA;
        public int AmountA
        {
            get { return _amountA; }
            set { this.RaiseAndSetIfChanged(ref _amountA, value); }
        }

        int _amountB;
        public int AmountB
        {
            get { return _amountB; }
            set { this.RaiseAndSetIfChanged(ref _amountB, value); }
        }

        int _amountC;
        public int AmountC
        {
            get { return _amountC; }
            set { this.RaiseAndSetIfChanged(ref _amountC, value); }
        }

        int _total;
        public int Total
        {
            get { return _total; }
            set { this.RaiseAndSetIfChanged(ref _total, value); }
        }

        public ICommand OkCommand { get; protected set; }

        public ExampleViewModel()
        {
            Observable.Merge(this.ObservableForProperty(x => x.AmountA),
                    this.ObservableForProperty(x => x.AmountB),
                    this.ObservableForProperty(x => x.AmountC))
            .Subscribe(_ => Total = AmountA + AmountB + AmountC);

            var canHitOk = this.WhenAny(
                x => x.Total,
                x => x.AmountA,
                (total, amountA) => total.Value > 100 && amountA.Value == 20);

            OkCommand = new ReactiveCommand(canHitOk);
        }
    }
}
