   M S B A M L   `   `   `  ���� E  ADragablz, Version=0.0.3.197, Culture=neutral, PublicKeyToken=null5(clr-namespace:Dragablz;assembly=DragablzDragablz  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity C ?MusicApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:MusicAppMusicApp =#clr-namespace:MusicApp.Song_BuilderMusicApp.Song_Builder    MusicApp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     =dragablz(clr-namespace:Dragablz;assembly=Dragablz     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     (localclr-namespace:MusicApp     U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  /songs#clr-namespace:MusicApp.Song_Builder  -   $��window��  � Title$  	Music app��$	��800��$	��500��$	��400��$	��300��   MaterialDesignPaper#���     MaterialDesignFont#���  $��13(�$��Regular"�  MaterialDesignBody#���  &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=� � WindowStartupLocation$ CenterScreen=� � DataContext "  MusicApp.MainWindowViewModel  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Closing��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *CloseWindowCommand+	 � 	Resources	    BooleanToVisibilityConverter��%   &	       �� .��� 
 � ColumnDefinitions
 �� $	��70* ��� $
��Auto ��� $	��60* �.+�� $��0�� � RowDefinitions �� $��* ��� $
��Auto �.+��� $��0��$��0q�$��Stretch=�$��Center=� �� Orientation$ Vertical=�y��� *IsFilesLoaded+ �� 	Converter# [ 	   labelTextBlockStyle �� BasedOn �� 
TargetType �� Property �� Value  textBlockStyle��%W  &	       7��&	 �     7���� 8   " ��.���  � ��$	 150����  �@��TextAlignment$ Right=���  � ��$ 5,5,10,5q���  � ��$ 16(���  � ��$ Center=��� 8   " ��.���  � ��$ 0,5,0,5q���  � ��$ Center=���  � ��$ 20(���  � ��$ SemiBold"���  �@��TextAlignment$
 Left=�.+��� $ 
Horizontal=�.+��� #��[ $��Title:���� #��[ ���� *#!Player.Instance.CurrentSong.Title+�� $ 
Horizontal=�.+��� #��[ $��Artist:���� #��[ ���� *$"Player.Instance.CurrentSong.Artist+�� $ 
Horizontal=�.+��� #��[ $��Genre:���� #��[ ���� *Player.CurrentSong.Genre+�� $ 
Horizontal=�.+��� #��[ $��Year:���� #��[ ���� *" Player.Instance.CurrentSong.Year+� $��1��{��� *IsFilesLoaded+
 �� $��90 ��� $��* ��� $��90 � �� $
��Auto ��� $��* �.+��� $��1��$��0��$��10,0,0,0q�$��Center=�$��Center=�$ Vertical=�{��� *IsFilesLoaded+.+�	� $��Random�� d� 	IsChecked �� *
PlayRandom+	� $��Normal�� �� *
PlayNormal+	� $��Reversed�� �� *PlayReversed+�� $��0��$��1��$��Center=�$��Center=�$ 
Horizontal=�{��� *IsFilesLoaded+	  �� Triggers P� Property P� Value��%   '��       7���� 8   " �� P�  � {� .  . ���  � ��
 �����P�  � {� . . ���  � ��
 �����.+��� -   $��songPositionTextBlock������ *Player.CurrentSongPosition+ �� StringFormat$ {0:mm':'ss}���� $	�� / ���� ���� *Player.CurrentSongLength+$ {0:mm':'ss}��� $��1��$��1��$��	5,20,5,20q�$��Center=�	 ��%a   '��       7���� 8   " ��.���  � ��$ 5q���  � ��$ 45����  � ��$ 50�� �� $��* ��� $��* �.+��� $��0��$��Center=�$ 
Horizontal=�.+��� $	��103������ *PlayPauseCommand+��'  !MaterialDesignThemes.Wpf.PackIcon  $��30��$��30���  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ 	PlayPause=��� $	��103������ *StopCommand+��  $��30��$��30��$
 Stop=��� $��1��$��Center=�$ 
Horizontal=�.+��� $��65��$��40������ *PrevCommand+��  $��22��$��22��$ SkipPrevious=��� $��65��$��40������ *NextCommand+{��� *Player.playList.IsDone+��  $��22��$��22��$ SkipNext=��� $��1��$��2��$��5q�$��Stretch=�
��� � �	   Dragablz.TabablzControl	  $��0��$��2��$
��10,5q�.@��� $��Playlist��.���  �� $��* ��� $
��Auto �.+��� $��0��$
��10,5q����� *Player.Songs+ �� SelectedItem �� *Player.CurrentSong+���� # [B��" �� P�  �@��
IsSelected . . ���  � ��$ 20(�P�  �@��
IsSelected .  . ���  � ��$ 12(����� .R��� $ Vertical=�.+��� ���� *
FileName+�� .+��� $��10,0,0,0q�$��11(����� ���� " �� ��  �� Binding ��  �� RelativeSource ��  �� AncestorType$ ListBoxItemL� �� Path$ 
IsSelected� �� Value True.a���  � y�$ Visible=���  ��  �� $ ListBoxItemL�$ 
IsSelected�	 False.a���  � y�$ 	Collapsed=��� $��1��$��5q�$��
Load files������ *LoadFilesCommand+�� $��History��.���  �� $��* ��� $
��Auto �.+��� $��0��$��Disabled=�$
��Auto=�.���� $��0��$
��10,5q����� *Player.SongsHistory+���� .R�� $��Stretch=�
 �� $��* ��� $
��Auto �.+��� $��0��$��Center=����� *
FileName+�� $��1��$��3q�$��0q�$��25��$��Right=�$��Center=�$��Right=�$��10(�����  �� ElementName$ window��$. (DataContext.RemoveSongFromHistoryCommand����� ��  $��15��$��15��$ Delete=�� $��1��$��5q�
 �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��0,3,3,0q����� *UndoHistoryCommand+��  $��15��$��15��$
 Undo=��� $��1��$��3,3,3,0q����� *RedoHistoryCommand+��  $��15��$��15��$
 Redo=��� $��3��$��3,3,0,0q����� *ClearHistoryCommand+���� $��Clear��