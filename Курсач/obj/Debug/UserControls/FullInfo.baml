   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      A%clr-namespace:Курсач.ViewModelsКурсач.ViewModels  5      (    "Курсач.UserControls.FullInfo   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Курсач.UserControls     5      ;	viewModel%clr-namespace:Курсач.ViewModels     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      �    	GridStyle#���   5        � DataContext  5
      �� *CurrentBook+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $
��1.4* �6    � RowDefinitions 5   
   �� 5      $
��1.8* �6   �� 5      $
��0.7* �6   �� 5      $��2* �6   �� 5      $��2* �6   .+�5   
   �� $
��Left=�6   $	��Top=�5      $��0��5      $��0��5      $��4��5      ��5      �� *COVER+�� 5   
   $-��'/Media/ЛентаАбонемент.png��6   $
��Left=�5      $	��Top=�5       J� Opacity 5      �� *Subscription+�� 5    
   $��0��6   $��1��5!      $��Stretch=�5"      .+�5#      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�5$      .+�5%      �� $��16(�6   $��15, 5, 5, 0q�5&        m_BookTitle#���  5'      �� 5(      $��16(�6   $	��300��5)      $
��Wrap=�5*      $��
0, 5, 0, 0q�5+      ��5,      �� *TITLE+5-      �� 5.      $��16(�6   $��15, 5, 0, 0q�5/      $
��Left=�51      ��50      �� *Genre+51      �� 52      $��15, 5, 0, 0q�6   $ 
Horizontal=�53      $
��Left=�54      .+�55      �� $��16(�6   $��
0, 0, 5, 0q�56        m_Author#���  57      �� 58      $��16(�6   ��59      �� *AUTHOR+5:      ]� 5;      .+�5<      �� $��Center=�6   $��15, 7.5, 0, 7.5q�5=      $
��Left=�5>      $ 
Horizontal=�5?      $��Stretch=�5@      .+�5A      �� $��16(�6    $��
0, 0, 5, 0q�5B       $��Center=�5D       ��5C       �� *RATING+5D       '  !MaterialDesignThemes.Wpf.PackIcon  5E      $��
0, 0, 5, 0q�6.      Kind$
 Star=�5F   .   $��Center=�5G   .   �� 5H      $��16(�6    $��
0, 0, 5, 0q�5I       $��Center=�5J       ��5K       �� *NUMBEROFVOICES+(  "MaterialDesignThemes.Wpf.RatingBar  5L      -   5          Name$ rating��5N   /   $��Center=�5L   /   $	��5 0q�5O   /   ���#B56F2F	   Value	 5M   /   �� *Mark+5P   /   5Q      �� 5R      $ 
Horizontal=�6   $��Center=�5S      .+�5T      �� -   5        
 ��Name$
 SetMark��5U        LightButton2#���  5T      $
��15 0q�5V      $	��200��5W      $��Right=�5Z        m_RateTheBook#���  5[      ��5X      �� *DataContext.MarkCommand+ �� RelativeSource ��  �� AncestorType" N���5Y      �� *Value+ �� ElementName$ rating��5\      5]      5^      5_      �� 5`   
   $��1��6   $��1��5a      $ 
Horizontal=�5b      $
��Left=�5c      .+�5d      ��   LightButton#���  6   $��15, 7, 0, 0q�5e      $	��200��5f      $
��Auto��5i        m_AddToBasket#���  5k      ��5g      �� * DataContext.AddToBasketCommand+ �� " N���5h      �� *	BOOK_ID+y�5j      �� *UserWithoutSubscription+5k      �� 5l        
DarkButton#���  6   $��15, 7, 0, 0q�5m      $	��200��5n        m_BuyTheBook#���  5q      $
��Auto��5s      ��5o      �� *DataContext.BuyCommand+ �� " N���5p      �� *	BOOK_ID+y�5r      �� *UserWithoutSubscription+5s      �� 5t      #���  6   $��15, 7, 0, 0q�5u      $	��200��5v       	 m_AddToShelf#��� 	 5z      $
��Auto��5{      ��5w      �� *#!DataContext.AddToYourBooksCommand+ �� " N���5x      �� *	BOOK_ID+y�5y      �� *UserWithSubscription+5{      5|      �� 5}   
   $��16(�6   $
��Wrap=�5~      $��Center=�5      $
��15 0q�5�      $��2��5�      $��2��5�      $��2��5�      ��5�      �� *DESCRIPTION+5�      5�      