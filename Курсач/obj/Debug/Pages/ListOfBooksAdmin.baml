   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Курсач.PagesКурсач.Pages  5      E'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      )    #Курсач.Pages.ListOfBooksAdmin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Курсач.Pages     5      @userControls'clr-namespace:Курсач.UserControls     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  ListOfBooksAdmin��5      .��5      �    GridMainStyle#���   6    � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      $��1* �6   �� 5      $��50 �6   .+�5   
   � $��0��6   $
��1200��6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $
��1.5* �6#   �� 5      $��1* �6#   �� 5      $
��2.5* �6#   .+�5      �� $	��0 5q�6     
ImageStyle#���  5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6'      Opacity$	 0.9Q�5   '      ShadowDepth$ 2Q�5   '   5!      �� 5"      -   5         ��Name$ Search��5$      $��Center=�5"      $��1��5#        m_Filter)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5%       % !MaterialDesignFloatingHintTextBox#��[ 5&      $��16(�5'      $��50��5(      ��5)      �� *Text+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 5*      $��2��6    
 m_Main#���  5+      $��Tahoma)�5,      $��Center=�5-      $��Center=�5.      $��24(�5/      �� 50      
 �� Orientation$
 
Horizontal=�6   $��3��51      $��Right=�52      .+�53      �� $��0q�54      $��0, 0, 10, 0q�55      $��16(�56      $	��180��57       �� SelectedIndex$ 0��58       �� MaxDropDownHeight$	 300��5;       �� SelectedItem 59      �� *SelectedGenre+��5:      �� *Genres+��5<      �� 5=      .R�5>      �� $��0q�6(   $��Bottom=�5?   (   $
��Left=�5@   (   ��5>   3   �� *GENRE+5@   (   5A      5C      �� 5D        m_Search#���  6   $��Center=�5E        LightButton2#���  5F      ��5G      �� *FindByGenreCommand+�� 5I        m_ClearFilter#���  6   $��Center=�5J      $	��150��5K      $��15, 0, 0, 0q�5L        LightButton#���  5M      ��5N      �� *ClearCommand+5P      5Q      �� 5R   
   $��4��6   $��0��5S      $��1��5T      .��5U      �� -   5         ��Name$ listOfBooks��5X      $��Disabled=�5W      $��200 0q�5Y      ��5U      �� *Books+ �� Mode$ TwoWay=� 5V      �� *SelectedBook+��5Z      �� 5[      .R�5\      �� ._�5]      /   )Курсач.UserControls.BookUserControl  $	��150��6;   $	��330��6G    J� InputBindings 5^      d� 5_   "   &   System.Windows.Input.MouseAction   LeftDoubleClick d� MouseAction# Z 6/    �� Command 5`   /   ��  �� RelativeSource ��  �� AncestorType$ ListBoxL� �� Path$ DataContext.OpenFullInfo�5b      5c      ��5e      �� 5f      .R�5g      � $��Center=�6$   5h      5j      5k      �� 5l   
   ���#B56F2F$��2��5m      $��0��5n      $��4��5o      �� 5p   
   $��2��6   $��Disabled=�5q      ��5r      �� 5s      .R�5t      � $��Center=�6    5u      .@�5w      �� .��5x      �� 
������ 
������ $��0q�5{      ��5z      �� *AddBookCommand+.��5|      �� $
 
Horizontal=�6!   .+�5}      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�62   $
��Auto��5~   .   $
��Auto��5   .   �� 5�       		 m_Add#��� 	 6$   $	��5 0q�5�   $   $��Center=�5�   $   5�      5�      5�      �� 5�      .��5�      �� 
������ 
������ $��0q�5�      ��5�      �� *GetBooksCommand+.��5�      �� $
 
Horizontal=�6!   .+�5�        $ TrayArrowDown=�62   $
��Auto��5�   .   $
��Auto��5�   .   �� 5�       
 	m_GetData#��� 
 6$   $	��5 0q�5�   $   $��Center=�5�   $   5�      5�      5�      �� 5�      .��5�      �� 
������ 
������ $��0q�5�      ��5�      �� *SetBooksCommand+��5�      �� *SelectedBook+.��5�      �� $
 
Horizontal=�6!   .+�5�        $ TrayArrowUp=�62   $
��Auto��5�   .   $
��Auto��5�   .   �� 5�        
m_SendData#���  6$   $	��5 0q�5�   $   $��Center=�5�   $   5�      5�      5�      �� 5�      .��5�      �� 
������ 
������ $��0q�5�      ��5�      �� *RemoveBookCommand+��5�      �� *SelectedBook+.��5�      �� $
 
Horizontal=�6!   .+�5�        $ Remove=�62   $
��Auto��5�   .   $
��Auto��5�   .   �� 5�        m_Remove#���  6$   $	��5 0q�5�   $   $��Center=�5�   $   5�      5�      5�      5�      5�      5�      