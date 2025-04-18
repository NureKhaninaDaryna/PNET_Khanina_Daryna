﻿using System.Windows.Input;
using UI.State.Navigators;

namespace UI.Commands;

public class RenavigateCommand : ICommand
{
    private readonly IRenavigator _renavigator;

    public RenavigateCommand(IRenavigator renavigator)
    {
        _renavigator = renavigator;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        _renavigator.Renavigate();
    }
}