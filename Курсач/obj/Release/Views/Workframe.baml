   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:КурсачКурсач  E'clr-namespace:Курсач.UserControlsКурсач.UserControls  A%clr-namespace:Курсач.ViewModelsКурсач.ViewModels  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections     Курсач.Workframe   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Курсач     @userControls'clr-namespace:Курсач.UserControls     7model%clr-namespace:Курсач.ViewModels     Bcol2clr-namespace:System.Collections;assembly=mscorlib     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Bookварь�� � WindowState$ 	Maximized=�$
��1410��$	��780�� � Icon$c ]C:\Users\User\Desktop\Курсааааач\Курсач\Курсач\Media\Логотип.png��.���    GridMainStyle#���    � ColumnDefinitions �� -    ��Name$	 col��$��60���� $	��80* ��� $	��400 ��� $	��400 ��� $	��80* � � RowDefinitions �� $��75 ��� $	��50* ��� $
��400* ��� $
��400* �.+��� $��0��$��6��$	��Top=�$��75�����#B56F2F�� $��Center=�$��0��$��6��$	��Top=�$��75�� �� Orientation$ 
Horizontal=�.+��� $1��+/Media/АбонементОбводка.png��$��0, 0, 20, 0q��� $��Center=�$��20(�
�������$��Center=�  m_SubscriptionAdvertisment#���   J� Effect -  'System.Windows.Media.Effects.BlurEffect     Radius �� *Blur+� -   	 �Name$	 	sidePanel��$��5�����#B56F2F$
��Left=�.+�]� .+��� $	��Top=�$	��Top=�.+��� $��Disabled=�
�������.@��� $��50��$��0q�.���� $��Center=�.+��� -   $	��180��$��50��$��0q�$��Center=�$
��Left=�
������ 
������ .���� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�$��10, 0, 10, 0q�$��40��$��40��
   Kind$

 Menu=��� $��Center=�$��16(� 
 m_Menu#���  �� $��0q�$��50��.���� $	��180��$��50��$��0q�$��Center=�$
��Left=�
������ 
������ ���� *OpenListOfBooksCommand+.���� $ 
Horizontal=�$��Center=�.+�  $

 Home=�$��40��$��40��$��	10,0,10,0q�$��Center=���  
 m_Main#���  $��Center=�$��16(��� $��0q�$��50��.���� $��0q�
������ 
������ $��Stretch=�$
��Left=�$��50��$	��180������ *OpenYourBooksCommand+.���� $ 
Horizontal=�$��Stretch=�.+�  $
 	Bookshelf=�$��40��$��40��$��	10,0,10,0q�$��Center=���   m_Shelf#���  $��Center=�$��16(��� $��0q�$��50��.���� $��0q�
������ 
������ $��Stretch=�$
��Left=�$��50��$	��180������ *OpenBasketCommand+.���� $ 
Horizontal=�$��Center=�.+�  $
 CartVariant=�$��40��$��40��$��	10,0,10,0q�$��Center=���   m_Basket#���  $��Center=�$��16(��� $��0q�$��50��.���� $��0q�
������ 
������ $��Stretch=�$
��Left=�$	��180��$��50������ *OpenUserCommand+.���� $ 
Horizontal=�$��Center=�.+�  $

 User=�$��40��$��40��$��	10,0,10,0q�$��Center=���  
 m_User#���  $��Center=�$��16(��� $��0q�$��50��.���� $��0q�
������ 
������ $��Stretch=�$
��Left=�$��50��$	��180��$��Stretch=����� *SubscriptionCommand+.���� $ 
Horizontal=�$��Center=�.+�  $
 Crown=�$��40��$��40��$��	10,0,10,0q�$��Center=���   m_Subscription#���  $��Center=�$��16(��� $��0q�$��50��.���� $��0q�
������ 
������ $��Stretch=�$
��Left=�$��50��$	��180������ *SettingsCommand+.���� $ 
Horizontal=�$��Center=�.+�  $
 Settings=�$��40��$��40��$��	10,0,10,0q�$��Center=���   
m_Settings#���  $��Center=�$��16(�    �� *Blur+ �   � NavigationUIVisibility$ Hidden=�$��1��$��1��$��4��$��Center=�$��Stretch=�$��3������ *CurrentPageViewModel+    �� *Blur+�� $��6��
���   �$��Stretch=�$��Stretch=�$
��Auto��$
��Auto�� J� Opacity$	 0.2Q�
������ $��0q�$��4��y��� *
Visibility+���� *CloseCommand+�� $��4��$��1��$��1��$��0, 0, 18, 0q�$��3��$	��600��$	��400��$��Center=�$��Center=����� *AddCreditCardViewModel+