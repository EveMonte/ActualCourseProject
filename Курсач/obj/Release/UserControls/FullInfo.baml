   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  A%clr-namespace:Курсач.ViewModelsКурсач.ViewModels  (    "Курсач.UserControls.FullInfo   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Курсач.UserControls     ;	viewModel%clr-namespace:Курсач.ViewModels     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  .���    	GridStyle#���     � DataContext  �� *CurrentBook+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� � ColumnDefinitions �� $��1* ��� $
��1.4* � � RowDefinitions �� $
��1.8* ��� $
��0.7* ��� $��2* ��� $��2* �.+��� $
��Left=�$	��Top=�$��0��$��0��$��4������ *COVER+�� $-��'/Media/ЛентаАбонемент.png��$
��Left=�$	��Top=� J� Opacity �� *Subscription+�� $��0��$��1��$��Stretch=�.+���  �� Orientation$ 
Horizontal=�$
��Left=�.+��� $��16(�$��15, 5, 5, 0q�  m_BookTitle#���  �� $��16(�$	��300��$
��Wrap=�$��
0, 5, 0, 0q����� *TITLE+�� $��16(�$��15, 5, 0, 0q�$
��Left=����� *Genre+�� $��15, 5, 0, 0q�$ 
Horizontal=�$
��Left=�.+��� $��16(�$��
0, 0, 5, 0q�  m_Author#���  �� $��16(����� *AUTHOR+]� .+��� $��Center=�$��15, 7.5, 0, 7.5q�$
��Left=�$ 
Horizontal=�$��Stretch=�.+��� $��16(�$��
0, 0, 5, 0q�$��Center=����� *RATING+'  !MaterialDesignThemes.Wpf.PackIcon  $��
0, 0, 5, 0q�   Kind$
 Star=�$��Center=��� $��16(�$��
0, 0, 5, 0q�$��Center=����� *NUMBEROFVOICES+(  "MaterialDesignThemes.Wpf.RatingBar  -     Name$ rating��$��Center=�$	��5 0q����#B56F2F	   Value	 �� *Mark+�� $ 
Horizontal=�$��Center=�.+��� -   
 ��Name$
 SetMark��  LightButton2#���  $
��15 0q�$	��200��$��Right=�  m_RateTheBook#���  ���� *DataContext.MarkCommand+ �� RelativeSource ��  �� AncestorType" N����� *Value+ �� ElementName$ rating���� $��1��$��1��$ 
Horizontal=�$
��Left=�.+���   LightButton#���  $��15, 7, 0, 0q�$	��200��$
��Auto��  m_AddToBasket#���  ���� * DataContext.AddToBasketCommand+ �� " N����� *	BOOK_ID+y��� *UserWithoutSubscription+��   
DarkButton#���  $��15, 7, 0, 0q�$	��200��  m_BuyTheBook#���  $
��Auto������ *DataContext.BuyCommand+ �� " N����� *	BOOK_ID+y��� *UserWithoutSubscription+�� #���  $��15, 7, 0, 0q�$	��200�� 	 m_AddToShelf#��� 	 $
��Auto������ *#!DataContext.AddToYourBooksCommand+ �� " N����� *	BOOK_ID+y��� *UserWithSubscription+�� $��16(�$
��Wrap=�$��Center=�$
��15 0q�$��2��$��2��$��2������ *DESCRIPTION+