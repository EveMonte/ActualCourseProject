   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      %    Курсач.UserControls.Admin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      3local'clr-namespace:Курсач.UserControls  5      .��5	      �� ._�5
   
   �    	GridStyle#���   6     � RowDefinitions  5      �� 5      $��15 �6    �� 5      $
��1.5* �6    �� 5      $��1* �6    �� 5      $
��1.5* �6    �� 5      $��15 �6     � ColumnDefinitions 5      �� 5      $��15 �6#   �� 5      $��1* �6#   �� 5      $��1* �6#   �� 5      $��15 �6#   .+�5      ]� $��3��6   $��1��5      $��4��5      $��Stretch=�5      $��Bottom=�5      .+�5      ��   LightButton#���  6   $
��10 0q�5      $
��Left=�5        	m_Confirm#���  5       $	��150��5#      ��5!      �� *DataContext.ChangeCommand+ �� RelativeSource ��  �� AncestorType" N���5"      �� *	USER_ID+5#      �� 5$        
DarkButton#���  6   $��Right=�5%      $
��10 0q�5&      $��Удалить��5'      $	��150��5(      ��5)      �� *DataContext.RemoveCommand+ �� " N���5*      �� *	USER_ID+5,      �� 5-       �� SelectedValuePath$ Content��6    �� SelectedIndex$ 1��5.      $��1��50      $	��200��51      $��14(�52      $��Bottom=�53      $��2��54      $��2��55       �� SelectedValue 5/      �� *	ACCOUNT+ �� Mode$ TwoWay=�.@�56      ��  
 m_User#���  6   �� 57        	m_Editors#���  6   58      �� 59       �� Orientation$ 
Horizontal=�6   $��1��5:      $��1��5;      $��2��5<      $��Center=�5=      .+�5>      '  !MaterialDesignThemes.Wpf.PackIcon  	   Kind$	 
AccountTie=�6*   $
��Auto��5?   *   $
��Auto��5@   *   �� 5A      .+�5B      �� $��16(�6    ��5C      �� *SECOND_NAME+�� 5D       
 m_Name)  #MaterialDesignThemes.Wpf.HintAssist
   Hint#
 �  6    $��16(�5E      ��5F      �� *NAME+�� 5G      $��16(�6    $��Center=�5H      ��5I      �� *EMAIL+5J      5K      5L      5M      5N      