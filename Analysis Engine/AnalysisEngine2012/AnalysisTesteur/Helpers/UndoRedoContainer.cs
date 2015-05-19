using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AnalysisTesteur.Helpers
{
    public class UndoRedoContainer : UIObject 
    {

        public UndoRedoContainer(object Owner)
        {
            this.Owner = Owner;
            this.UndoList = new ObservableCollection<UndoRedoToken>();
            this.RedoList = new ObservableCollection<UndoRedoToken>();
        }
        public UndoRedoContainer()
        {
            this.UndoList = new ObservableCollection<UndoRedoToken>();
            this.RedoList = new ObservableCollection<UndoRedoToken>();
        }
        public object Owner { get; private set; }

        public ObservableCollection<UndoRedoToken> UndoList { get; private set; }
       
        public ObservableCollection<UndoRedoToken> RedoList { get; private set; }

        public bool CanUndo { get { return this.UndoList.Count > 0; } }

        public bool CanRedo { get { return this.RedoList.Count > 0; } }

        public void Push(UndoRedoToken token)
        {
            RedoList.Clear();

            UndoList.Add(token);

            CommandManager.InvalidateRequerySuggested();
        }
        public void Push(Action undo, Action redo, string caption = "")
        {
            var token = new UndoRedoToken();

            token.Caption = caption;

            token.UndoAction =(object o1, object o2) => undo();

            token.RedoAction = (object o1, object o2) => redo();

            this.Push(token);
        }
        public void Push(Action<object> undo, Action<object> redo, object parameter, string caption = "")
        {
            var token = new UndoRedoToken();

            token.Caption = caption;

            token.UndoAction = (object o1, object o2) => undo(o2);

            token.RedoAction = (object o1, object o2) => redo(o2);

            token.Parameter = parameter;

            this.Push(token);

        }
             
        public void Undo()
        {
            if (!CanUndo) return;

            var token = UndoList.Last();

            UndoList.Remove(token);

            RedoList.Add(token);

            token.UndoAction(this.Owner, token.Parameter);
        }
        public void Redo()
        {
            if (!CanRedo) return;

            var token = RedoList.Last();

            RedoList.Remove(token);

            UndoList.Add(token);

            token.RedoAction(this.Owner, token.Parameter);
        }
    }

    public class UndoRedoToken : UIObject 
    {
        private string _Caption;
        public string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                _Caption = value;
                OnPropertyChanged("Caption");
            }
        }

        private Object _Parameter;
        public Object Parameter
        {
            get
            {
                return _Parameter;
            }
            set
            {
                _Parameter = value;
                OnPropertyChanged("Parameter");
            }
        }

        private Action<object,object> _UndoAction;
        public Action<object,object> UndoAction
        {
            get
            {
                return _UndoAction;
            }
            set
            {
                _UndoAction = value;
                OnPropertyChanged("UndoAction");
            }
        }
        private Action<object,object> _RedoAction;
        public Action<object,object> RedoAction
        {
            get
            {
                return _RedoAction;
            }
            set
            {
                _RedoAction = value;
                OnPropertyChanged("RedoAction");
            }
        }   


    }
}
