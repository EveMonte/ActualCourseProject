   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Курсач.PagesКурсач.Pages  E'clr-namespace:Курсач.UserControlsКурсач.UserControls  )    #Курсач.Pages.ListOfBooksAdmin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:Курсач.Pages     @userControls'clr-namespace:Курсач.UserControls     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    N� Title$  ListOfBooksAdmin��.���    GridMainStyle#���    � RowDefinitions �� $��70 ��� $��1* ��� $��50 �.+�� $��0��$
��1200�� � ColumnDefinitions �� $��1* ��� $
��1.5* ��� $��1* ��� $
��2.5* �.+��� $	��0 5q�  
ImageStyle#���   J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   Opacity$	 0.9Q�   ShadowDepth$ 2Q��� -    ��Name$ Search��$��Center=�$��1��  m_Filter)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �   % !MaterialDesignFloatingHintTextBox#��[ $��16(�$��50������ *Text+	 �� UpdateSourceTrigger$	 PropertyChanged=��� $��2�� 
 m_Main#���  $��Tahoma)�$��Center=�$��Center=�$��24(��� 
 �� Orientation$
 
Horizontal=�$��3��$��Right=�.+��� $��0q�$��0, 0, 10, 0q�$��16(�$	��180�� �� SelectedIndex$ 0�� �� MaxDropDownHeight$	 300�� �� SelectedItem �� *SelectedGenre+���� *Genres+���� .R��� $��0q�$��Bottom=�$
��Left=����� *GENRE+��   m_Search#���  $��Center=�  LightButton2#���  ���� *FindByGenreCommand+��   m_ClearFilter#���  $��Center=�$	��150��$��15, 0, 0, 0q�  LightButton#���  ���� *ClearCommand+�� $��4��$��0��$��1��.���� -    ��Name$ listOfBooks��$��Disabled=�$��200 0q����� *Books+ �� Mode$ TwoWay=� �� *SelectedBook+���� .R��� ._�/   )Курсач.UserControls.BookUserControl  $	��150��$	��330�� J� InputBindings d� &   System.Windows.Input.MouseAction   LeftDoubleClick d� MouseAction# Z  �� Command ��  �� RelativeSource ��  �� AncestorType$ ListBoxL� �� Path$ DataContext.OpenFullInfo����� .R�� $��Center=��� ���#B56F2F$��2��$��0��$��4���� $��2��$��Disabled=����� .R�� $��Center=�.@��� .���� 
������ 
������ $��0q����� *AddBookCommand+.���� $
 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�$
��Auto��$
��Auto����  		 m_Add#��� 	 $	��5 0q�$��Center=��� .���� 
������ 
������ $��0q����� *GetBooksCommand+.���� $
 
Horizontal=�.+�  $ TrayArrowDown=�$
��Auto��$
��Auto����  
 	m_GetData#��� 
 $	��5 0q�$��Center=��� .���� 
������ 
������ $��0q����� *SetBooksCommand+���� *SelectedBook+.���� $
 
Horizontal=�.+�  $ TrayArrowUp=�$
��Auto��$
��Auto����   
m_SendData#���  $	��5 0q�$��Center=��� .���� 
������ 
������ $��0q����� *RemoveBookCommand+���� *SelectedBook+.���� $
 
Horizontal=�.+�  $ Remove=�$
��Auto��$
��Auto����   m_Remove#���  $	��5 0q�$��Center=�