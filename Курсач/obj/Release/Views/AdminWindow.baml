   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:КурсачКурсач      Курсач.AdminWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  &localclr-namespace:Курсач    � Title$  Bookварь (for admins)�� � WindowState$ 	Maximized=� � Icon$c ]C:\Users\User\Desktop\Курсааааач\Курсач\Курсач\Media\Логотип.png��$
��1410��$	��780��.���    GridMainStyle#���    � ColumnDefinitions �� -    ��Name$	 col��$��60���� $	��80* ��� $	��400 ��� $	��400 ��� $	��80* � � RowDefinitions �� $	��50* ��� $
��400* ��� $
��400* �.+�� -    �Name$ 	sidePanel��$��5�����#B56F2F$
��Left=�.+��� $��Disabled=�
�������.@��� $��50��$��0q�.���� $��Center=�.+��� -   $	��180��$��50��$��0q�$��Center=�$
��Left=�
������ 
������ .����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�$��10, 0, 10, 0q�$��40��$��40��   Kind$
 Menu=��� $��Center=�$��16(� 
 m_Menu#���  �� $��0q�$��50��.���� 
������ 
������ $��0q�$��Center=�$
��Left=�$
��Left=�$	��180��$��50������ *OpenBooksCommand+.���� $ 
Horizontal=�$��Center=�.+�  $ BookMultiple=�$��40��$��40��$��	10,0,10,0q�$��Center=��� $��
Книги��$��Center=�$��16(��� $��0q�$��50��y��� *ButtonVisibility+.���� 
������ 
������ $��0q�$��Center=�$
��Left=�$
��Left=�$	��180��$��50������ *OpenAdminsCommand+.���� $ 
Horizontal=�$��Center=�.+�  $ 
AccountTie=�$��40��$��40��$��	10,0,10,0q�$��Center=���   	m_Editors#���  $��Center=�$��16(��� $��0q�$��50��.���� 
������ 
������ $��0q�$��Center=�$
��Left=�$
��Left=�$	��180��$��50������ *OpenUsersCommand+.���� $ 
Horizontal=�$��Center=�.+�  $ AccountGroup=�$��40��$��40��$��	10,0,10,0q�$��Center=���   m_Users#���  $��Center=�$��16(��� $��0q�$��50��.���� 
������ 
������ $��0q�$��Center=�$
��Left=�$
��Left=�$	��180��$��50������ *OpenUserCommand+.���� $ 
Horizontal=�$��Center=�.+�  $
 User=�$��40��$��40��$��	10,0,10,0q�$��Center=���  
 m_User#���  $��Center=�$��16(��� $��0q�$��50��.���� 
������ 
������ $��0q�$��Center=�$
��Left=�$
��Left=�$	��180��$��50������ *OpenAdvertisementCommand+.���� $ 
Horizontal=�$��Center=�.+�  $ Store=�$��40��$��40��$��	10,0,10,0q�$��Center=���   m_Advertisement#���  $��Center=�$��16(��� $��0q�$��50��.���� 
������ 
������ $��0q�$��Center=�$
��Left=�$
��Left=�$	��180��$��50������ *OpenSettingsCommand+.���� $ 
Horizontal=�$��Center=�.+�  $ Settings=�$��40��$��40��$��	10,0,10,0q�$��Center=���   
m_Settings#���  $��Center=�$��16(�	 J� Effect	 -  'System.Windows.Media.Effects.BlurEffect  
   Radius
 �� *Blur+ � $��1��$��1��$��4��$��Center=�$��Stretch=�$��3��  � NavigationUIVisibility$ Hidden=�$	��0 0q����� *CurrentPageViewModel+	   
 �� *Blur+�� $��6��
���   �$��Stretch=�$��Stretch=�$
��Auto��$
��Auto�� J� Opacity$	 0.2Q�
������ $��0q�$��4��y��� *
Visibility+���� *CloseCommand+�� $��4��$��1��$��1��$��0, 0, 18, 0q�$��3��$	��600��$	��400��$��Center=�$��Center=����� *AddCreditCardViewModel+