   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  %    Курсач.UserControls.Admin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  3local'clr-namespace:Курсач.UserControls  .���� ._��    	GridStyle#���     � RowDefinitions  �� $��15 ��� $
��1.5* ��� $��1* ��� $
��1.5* ��� $��15 � � ColumnDefinitions �� $��15 ��� $��1* ��� $��1* ��� $��15 �.+�]� $��3��$��1��$��4��$��Stretch=�$��Bottom=�.+���   LightButton#���  $
��10 0q�$
��Left=�  	m_Confirm#���  $	��150������ *DataContext.ChangeCommand+ �� RelativeSource ��  �� AncestorType" N����� *	USER_ID+��   
DarkButton#���  $��Right=�$
��10 0q�$��Удалить��$	��150������ *DataContext.RemoveCommand+ �� " N����� *	USER_ID+��  �� SelectedValuePath$ Content�� �� SelectedIndex$ 1��$��1��$	��200��$��14(�$��Bottom=�$��2��$��2�� �� SelectedValue �� *	ACCOUNT+ �� Mode$ TwoWay=�.@���  
 m_User#���  ��   	m_Editors#���  ��  �� Orientation$ 
Horizontal=�$��1��$��1��$��2��$��Center=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  	   Kind$	 
AccountTie=�$
��Auto��$
��Auto���� .+��� $��16(����� *SECOND_NAME+��  
 m_Name)  #MaterialDesignThemes.Wpf.HintAssist
   Hint#
 �  $��16(����� *NAME+�� $��16(�$��Center=����� *EMAIL+