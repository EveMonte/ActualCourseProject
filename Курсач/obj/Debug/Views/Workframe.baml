   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:КурсачКурсач  5   	   E'clr-namespace:Курсач.UserControlsКурсач.UserControls  5   	   A%clr-namespace:Курсач.ViewModelsКурсач.ViewModels  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5	   	       Курсач.Workframe   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Курсач     5   	   @userControls'clr-namespace:Курсач.UserControls     5   	   7model%clr-namespace:Курсач.ViewModels     5   	   Bcol2clr-namespace:System.Collections;assembly=mscorlib     5	   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Bookварь��5   	    � WindowState$ 	Maximized=�5   	   $
��1410��5   	   $	��780��6    � Icon$c ]C:\Users\User\Desktop\Курсааааач\Курсач\Курсач\Media\Логотип.png��5   	   .��5   
   �    GridMainStyle#���   6    � ColumnDefinitions 5      �� 5      -   5         ��Name$	 col��5   #   $��60��60   �� 5      $	��80* �6#   �� 5      $	��400 �6#   �� 5      $	��400 �6#   �� 5      $	��80* �6    � RowDefinitions 5      �� 5      $��75 �6    �� 5      $	��50* �6    �� 5      $
��400* �6    �� 5      $
��400* �6    .+�5   
   �� $��0��6   $��6��5      $	��Top=�5       $��75��5!      ���#B56F2F5"      �� 5#   
   $��Center=�6   $��0��5$      $��6��5%      $	��Top=�5&      $��75��5'       �� Orientation$ 
Horizontal=�5(      .+�5)      �� $1��+/Media/АбонементОбводка.png��6   $��0, 0, 20, 0q�5*      �� 5+      $��Center=�6   $��20(�5,      
�������$��Center=�5.        m_SubscriptionAdvertisment#���  5/       J� Effect 50      -  'System.Windows.Media.Effects.BlurEffect  51         Radius 6   �� *Blur+53      � 54      -   5        	 �Name$	 	sidePanel��54      $��5��55      ���#B56F2F$
��Left=�57      .+�58      ]� .+�59      �� $	��Top=�6!   $	��Top=�5:      .+�5;      �� $��Disabled=�6#   
�������.@�5=      �� $��50��6+   $��0q�5>   '   .��5?   "   �� $��Center=�6-   .+�5@   &   �� -   5        $	��180��5A   )   $��50��5B   )   $��0q�5C   )   $��Center=�5D   )   $
��Left=�5E   )   
������ 
������ .��5H   *   �� $ 
Horizontal=�65   .+�5I   .   '  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�6F   $��10, 0, 10, 0q�5J   B   $��40��5K   B   $��40��5L   B   
   Kind$

 Menu=�5M   B   �� 5N   .   $��Center=�68   $��16(�6S    
 m_Menu#���  6a   5O   +   5P   '   5Q   #   5R      �� 5S      $��0q�6+   $��50��5T   '   .��5U      �� $	��180��6%   $��50��5V   )   $��0q�5W   )   $��Center=�5X   )   $
��Left=�5Y   )   
������ 
������ ��5\   %   �� *OpenListOfBooksCommand+.��5]   &   �� $ 
Horizontal=�61   $��Center=�5^   )   .+�5_   *     $

 Home=�6B   $��40��5`   >   $��40��5a   >   $��	10,0,10,0q�5b   >   $��Center=�5c   >   �� 5d   *    
 m_Main#���  64   $��Center=�5e   0   $��16(�5f   0   5g   '   5h   #   5i      �� 5j      $��0q�6+   $��50��67   .��5k   "   �� $��0q�6)   
������ 
������ $��Stretch=�5n   %   $
��Left=�5o   %   $��50��5p   %   $	��180��5q   %   ��5r   %   �� *OpenYourBooksCommand+.��5s   &   �� $ 
Horizontal=�61   $��Stretch=�5t   )   .+�5u   *     $
 	Bookshelf=�6B   $��40��5v   >   $��40��5w   >   $��	10,0,10,0q�5x   >   $��Center=�5y   >   �� 5z   *     m_Shelf#���  64   $��Center=�5{   0   $��16(�5|   0   5}   '   5~   #   5      �� 5�      $��0q�6+   $��50��67   .��5�   "   �� $��0q�6)   
������ 
������ $��Stretch=�5�   %   $
��Left=�5�   %   $��50��5�   %   $	��180��5�   %   ��5�   %   �� *OpenBasketCommand+.��5�   &   �� $ 
Horizontal=�61   $��Center=�5�   )   .+�5�   *     $
 CartVariant=�6B   $��40��5�   B   $��40��5�   B   $��	10,0,10,0q�5�   B   $��Center=�5�   B   �� 5�   *     m_Basket#���  64   $��Center=�5�   4   $��16(�5�   4   5�   '   5�   #   5�      �� 5�      $��0q�6+   $��50��5�   +   .��5�   "   �� $��0q�6)   
������ 
������ $��Stretch=�5�   )   $
��Left=�5�   )   $	��180��5�   )   $��50��5�   )   ��5�   )   �� *OpenUserCommand+.��5�   &   �� $ 
Horizontal=�61   $��Center=�5�   )   .+�5�   *     $

 User=�6B   $��40��6N   $��40��6Y   $��	10,0,10,0q�6e   $��Center=�5�   :   �� 5�   *    
 m_User#���  64   $��Center=�6T   $��16(�6o   5�   '   5�   #   5�      �� 5�      $��0q�6+   $��50��67   .��5�   "   �� $��0q�6)   
������ 
������ $��Stretch=�5�   %   $
��Left=�5�   %   $��50��5�   %   $	��180��5�   %   $��Stretch=�5�   %   ��5�   %   �� *SubscriptionCommand+.��5�   &   �� $ 
Horizontal=�61   $��Center=�5�   )   .+�5�   *     $
 Crown=�6B   $��40��6P   $��40��6[   $��	10,0,10,0q�6g   $��Center=�5�   :   �� 5�   *     m_Subscription#���  64   $��Center=�6\   $��16(�6w   5�   '   5�   #   5�      �� 5�      $��0q�6+   $��50��67   .��5�   "   �� $��0q�6)   
������ 
������ $��Stretch=�5�   %   $
��Left=�5�   %   $��50��5�   %   $	��180��5�   %   ��5�   %   �� *SettingsCommand+.��5�   &   �� $ 
Horizontal=�61   $��Center=�5�   )   .+�5�   *     $
 Settings=�6B   $��40��6R   $��40��6]   $��	10,0,10,0q�6i   $��Center=�5�   :   �� 5�   *     
m_Settings#���  64   $��Center=�6X   $��16(�6s   5�   '   5�   #   5�      5�      5�      5�       5�        5�       6   �� *Blur+5�       � 5�   
     � NavigationUIVisibility$ Hidden=�6   $��1��5�      $��1��5�      $��4��5�      $��Center=�5�      $��Stretch=�5�      $��3��5�      ��5�      �� *CurrentPageViewModel+ 5�        5�       6   �� *Blur+5�      �� 5�   
   $��6��6   
���   �$��Stretch=�5�      $��Stretch=�5�      $
��Auto��5�      $
��Auto��5�       J� Opacity$	 0.2Q�5�      
������ $��0q�5�      $��4��5�      y�5�      �� *
Visibility+��5�      �� *CloseCommand+5�      �� 5�   
   $��4��5�      $��1��5�      $��1��5�      $��0, 0, 18, 0q�5�      $��3��5�      $	��600��5�      $	��400��5�      $��Center=�5�      $��Center=�5�      ��5�      �� *AddCreditCardViewModel+5�      5�      5�      