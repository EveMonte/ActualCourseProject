   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      *    $Курсач.UserControls.BasketBook   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      3local'clr-namespace:Курсач.UserControls  5      .��5	      �    	GridStyle#���   6     � ColumnDefinitions  5
   
   �� 5      $
��1.5* �6   �� 5      $��2* �6   �� 5      $��10 �6    � RowDefinitions 5   
   �� 5      $
��1.9* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��75 �6   .+�5   
   �� $
��Left=�6   $	��Top=�5      $��0��5      $��0��5      $��4��5      ��5      �� *COVER+�� 5   
   $��0��6   $��1��5      $��Stretch=�5      .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��16(�6   $��15, 5, 5, 0q�5         m_BookTitle#���  5!      �� 5"      $��16(�6   $
��Wrap=�5#      $	��300��5$      $��
0, 5, 0, 0q�5%      ��5&      �� *TITLE+5'      �� 5(      $��16(�6   $��15, 5, 0, 0q�5)      ��5*      �� *Genre+�� 5+      $��15, 5, 0, 0q�6   $ 
Horizontal=�5,      .+�5-      �� $��16(�6   $��
0, 0, 5, 0q�5.        m_Author#���  5/      �� 50      $��16(�6   ��51      �� *AUTHOR+52      ]� 53      .+�54      �� $��Center=�6   $��15, 7.5, 0, 7.5q�55      $
��Left=�56      $ 
Horizontal=�57      $��Stretch=�58      .+�59      �� $��16(�6    $��
0, 0, 5, 0q�5:       $��Center=�5<       ��5;       �� *RATING+5<       '  !MaterialDesignThemes.Wpf.PackIcon  5=      $��
0, 0, 5, 0q�6.      Kind$
 Star=�5>   .   $��Center=�5?   .   �� 5@      $��16(�6    $��
0, 0, 5, 0q�5A       $��Center=�5B       ��5C       �� *NUMBEROFVOICES+(  "MaterialDesignThemes.Wpf.RatingBar  5D      -   5          Name$ rating��5F   /   $��Center=�5D   /   $��Right=�5G   /   ���#B56F2F   Value 5E   /   �� *Mark+ �� Mode$ TwoWay=�5H   /   5I      �� 5J      $ 
Horizontal=�6   $��Right=�5K      $��Center=�5L      $��Right=�5M      .+�5N      �� -   5         ��Name$ SetMark��5O        LightButton2#���  5N      $
��15 0q�5P      $	��200��5Q      $��Right=�5T        m_RateTheBook#���  5U      ��5R      �� *DataContext.MarkCommand+ �� RelativeSource �� 	 �� AncestorType"	 N���5S      �� *	BOOK_ID+5V      5W      5X      5Y      �� 5Z   
   $��3��6   $��1��6"   $ 
Horizontal=�5[      $��10q�5\      $��Center=�5]      .+�5^      ��   LightButton#���  6   $	��200��5_      $��50��5`      $��16(�5a      $��0q�5d        m_BuyTheBook#���  5e      ��5b      �� *DataContext.BuyCommand+ �� "	 N���5c      �� *	BOOK_ID+5e      �� 5f        
DarkButton#���  6   $	��200��5g      $��20, 0, 0, 0q�5h      $��16(�5i        m_RemoveFromBasket#���  5j      $��50��5m      ��5k      �� *DataContext.DeleteCommand+ �� "	 N���5l      �� *	BOOK_ID+5m      5n      �� 5o   
   $��16(�6   $
��Wrap=�5p      $	��Top=�5q      $��1��5r      $��15, 5, 10, 0q�5s      $��2��5t      $��1��5u      ��5v      �� *DESCRIPTION+5w      5x      