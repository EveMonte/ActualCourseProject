   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:КурсачКурсач  5      ;"clr-namespace:Курсач.HelpersКурсач.Helpers  5	      "    Курсач.RegisterControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Курсач     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      3
components"clr-namespace:Курсач.Helpers  5	      .��5      �    	GridStyle#���   6     � RowDefinitions  5   
   �� 5      $
��0.9* �6   �� 5      $
��0.9* �6   �� 5      $
��1.1* �6   �� 5      $
��0.9* �6   �� 5      $��1* �6   �� 5      $
��0.6* �6   .+�5   
   � $��Stretch=�6   $��0��6-    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��0��6   $��0��5      .+�5      �� -   5         ��Name$ secondNameTextBox��5      $��50��5      $��0��5      $��	18,0,10,0q�5        �� SelectionBrush
 �  ��$��Stretch=�5"       �� 
CaretBrush
 �  ��  m_SecondNameShort)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5$       % !MaterialDesignFloatingHintTextBox#��[ 5%      $��16(�5&      
������  �� MaxLines$ 1��5(      ��5      �� *
SecondName+.��5(   '   5*      �� 5+        ErrorMessage#���  6   $��Center=�5,      ��5-      �� *SecondNameValidation+5.      �� 5/      $��0��6   $��1��50      .+�51      �� -   5        $ nameTextBox��51      $��50��52      $��1��53      $��Stretch=�54      $��	10,0,18,0q�56        m_NameShort# �  57      #��[ 58      $��16(�59      ��55      �� *Name+59      �� 5:      #���  6   $��Center=�5;      ��5<      �� *NameValidation+5=      5?      �� 5@   
   $��1��6   .+�5A      �� -   5        $ emailTextBox��5C      $��1��5A      $��50��5B      $
��18,0q�5E      $ Email��5F      #��[ 5G      $��16(�5H      ��5D      �� *Email+5H      �� 5I      $��Center=�5J      #���  5K      ��5I      �� *EmailValidation+5K      5L      �� 5N   
   $��2��6   .+�5O      G� -   5         G�Name$ password1Box��5P      $��50��5O      $
��18,0q�5Q       ) %MaterialDesignFloatingHintPasswordBox#���  5R        
m_Password# �  5S      $��16(�5T      *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5U      5   /Курсач.Helpers.PasswordBoxBindingBehavior  5V      	   Password	 6<   �� *FirstPassword+5X      �� 5Y      #���  6   $
��Wrap=�5Z      $	��400��5[      ��5\      �� *PasswordValidation+5]      �� 5^   
   $��3��6   .+�5_      G� -   5        $ password2Box��5`      $��50��5_      $
��18,0q�5a      #���  5b        m_RepeatPassword# �  5c      $��16(�5d       5e        5f      	 6<   �� *SecondPassword+5h      �� 5i      $��Center=�5j      #���  5k      ��5i      �� *SecondPasswordValidation+5k      5l      �� 5n   
   -   5        
 ��Name$
 Create_Account��5o      $��4��5n        
DarkButton#���  5p      $
��Auto��5r      $
��15 7q�5s      $��20(�5t       	 m_CreateAnAccount#��� 	 5u      ��5q      �� *RegistrationCommand+5u      �� 5w   
   $��5��6   .+�5x      �� $	��120��6   $��0q�6!   
������ 
������ ��5{      �� *OpenSignInCommand+.��5|      ��  � Cursor$
 Hand��6   $��Center=�5}      $��0,0,0,0q�5~       
 m_AlreadyHaveAnAccount#��� 
 5      $��14(�5�      ���	#FF1877F35�      5�      5�      5�      5�      