   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      (    "Курсач.UserControls.YourBook   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      3local'clr-namespace:Курсач.UserControls  5      .��5
      �    	GridStyle#���   6     � ColumnDefinitions  5   
   �� 5      $
��1.5* �6   �� 5      $��2* �6   �� 5      $��10 �6    � RowDefinitions 5   
   �� 5      $
��1.9* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��75 �6   .+�5   
   �� $
��Left=�6   $	��Top=�5      $��0��5      $��0��5      $��4��5      ��5      �� *COVER+�� 5   
   $��0��6   $��1��5      $��Stretch=�5      .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5       �� $��16(�6   $��15, 5, 5, 0q�5!        m_BookTitle#���  5"      �� 5#      $��16(�6   $	��300��5$      $
��Wrap=�5%      $��
0, 5, 0, 0q�5&      ��5'      �� *TITLE+5(      �� 5)      $��16(�6   $��15, 5, 0, 0q�5*      ��5+      �� *Genre+�� 5,      $��15, 5, 0, 0q�6   $ 
Horizontal=�5-      .+�5.      �� $��16(�6   $��
0, 0, 5, 0q�5/        m_Author#���  50      �� 51      $��16(�6   ��52      �� *AUTHOR+53      ]� 54      .+�55      �� $��Center=�6   $��15, 7.5, 0, 7.5q�56      $
��Left=�57      $ 
Horizontal=�58      $��Stretch=�59      .+�5:      �� $��16(�6    $��
0, 0, 5, 0q�5;       $��Center=�5=       ��5<       �� *RATING+5=       '  !MaterialDesignThemes.Wpf.PackIcon  5>      $��
0, 0, 5, 0q�6.      Kind$
 Star=�5?   .   $��Center=�5@   .   �� 5A      $��16(�6    $��
0, 0, 5, 0q�5B       $��Center=�5C       ��5D       �� *NUMBEROFVOICES+(  "MaterialDesignThemes.Wpf.RatingBar  5E      -   5          Name$ rating��5G   /   $��Center=�5E   /   $��Right=�5H   /   ���#B56F2F   Value 5F   /   �� *Mark+ �� Mode$ TwoWay=�5I   /   5J      �� 5K      $ 
Horizontal=�6   $��Right=�5L      $��Center=�5M      $��Right=�5N      .+�5O      �� -   5         ��Name$ SetMark��5P        LightButton2#���  5O      $
��15 0q�5Q      $	��200��5R      $��Right=�5U        m_RateTheBook#���  5V      ��5S      �� *DataContext.MarkCommand+ �� RelativeSource �� 	 �� AncestorType"	 N���5T      �� *	BOOK_ID+5W      5X      5Y      5Z      �� 5[   
     
DarkButton#���  6   $��3��5\      $��1��5]      $
��Auto��5^      $��50��5_       
 m_Open#���  5`      $��0q�5a      $��18(�5b      ��5c      �� *DataContext.DownloadCommand+ �� "	 N���5d      �� *	BOOK_ID+�� 5e   
   $��16(�6   $
��Wrap=�5f      $	��Top=�5g      $��1��5h      $��15, 5, 10, 0q�5i      $��2��5j      $��1��5k      ��5l      �� *DESCRIPTION+5m      5n      