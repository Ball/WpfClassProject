Wpf Class Project
========================================================

This project is meant to be used as a starting point for the students of my Wpf Class.
Each week, I'll update this project with new helper classes, data, and the week's assignmnet.
This project will not contain a complete application.

Architecture choices are made for educational reasons.

Week 1
---------------------------------------------------------
Students are expected to "draw" a picture of their own design with xaml.
This week's goal is to explore Xaml and basic graphical primitives.

Week 2
---------------------------------------------------------
Start of Wpf Tunes: a Wpf based iTunes clone with actual, but limited, functionality.
This week's goal is to explore basic controls and layout types.
The assignment is to replicate the layout of WpfTunes as pictured in the file
AssignmentReferences\Week1Goal.png

Week 3
---------------------------------------------------------
Will it Blend?
This week's goal is to bind the UI elements to a back-end data structure.
If you've used blend before, I recommend trying it with a xaml initialized data context.
Set an instance of ShellDesignData to the Shell's DataContext and bind to get WPF to match
the Blend screenshot in AssignmentReferences\Week2Goal.png

Week 4
---------------------------------------------------------
With Style and Grace
This week's goal is to make the application look good!
* Give the play/pause/stop button the appropriate symbols
  (we'll use triggers and visible layers)
* The next and previous buttons need to look correct
* Buttons need a "pressed" and "mouseover"
* The main frame needs to have a subtle gradient
* The Playlist needs to be BLUE
See a screenshot in AssignmentReferences\Week3Goal.png

Week 5
---------------------------------------------------------
It's all about control
This week, you'll create two controls and a multivalue converter.
There where some changes to the data contexts directory, so be sure to update.
  1) PlayerControl - A UserControl that wraps the top of the player (back,next,playpausestop,title,and scrubber).
     Picture Included.
  2) PlayPauseStopButton - A subclass of button
     Using VS to create a CustomControl should create a /Themes/Generic.xaml file. Use it to set the template
     you wrote last time on your new button.  Add a template that uses NextAction (described below) to pick which
     picture to show.
     It should have the following dependency properties,
        ICommand PlayCommand
        ICommand PauseCommand
        ICommand StopCommand
        PlayStatus NextAction (identifies the command that CAN happen in this order Play,Pause,Stop)
     When each command is changed, you'll need to add a watcher to it's CanExecuteChanged event to trigger
     and update to the NextAction property.  This is how the button will update appropriately.  Below is a
     starting point.  You'll need to write PlayPauseStopButton#UpdateState to pick the proper NextAction.
        public static readonly DependencyProperty StopCommandProperty =
            DependencyProperty.Register("StopCommand",
                                        typeof (ICommand),
                                        typeof (PlayPauseStopButton),
                                        new PropertyMetadata(OnCommandChanged));
        public ICommand StopCommand
        {
            get { return (ICommand)GetValue(StopCommandProperty); }
            set { SetValue(StopCommandProperty, value); }
        }
        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as PlayPauseStopButton;
            if (self == null) return;
            var newCommand = e.NewValue as ICommand;
            if (newCommand != null)
                newCommand.CanExecuteChanged += (s, _) => self.UpdateState();
        }
  3) PlayParameterConverter - An IMultiValueConverter that enables the following xaml in the Shell.xaml
     It should generate an instance of PlayCommandParameter.
        <Controls:PlayerControl DockPanel.Dock="Top"
                    DataContext="{Binding CurrentSong}">
            <Controls:PlayerControl.PlayCommandParameter>
                <MultiBinding>
                    <MultiBinding.Converter>
                        <Converters:PlayParameterConverter/>
                    </MultiBinding.Converter>
                    <Binding Path="CurrentSong"/>
                    <Binding Path="SelectedItem" ElementName="listBox"/>
                    <Binding Path="SelectedItem" ElementName="listView"/>
                </MultiBinding>
            </Controls:PlayerControl.PlayCommandParameter>
        </Controls:PlayerControl>