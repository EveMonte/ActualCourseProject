   M S B A M L   `   `   `  ����G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Курсач.UserControlsКурсач.UserControls  5      .    (Курсач.UserControls.ForgotPassword   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      3local'clr-namespace:Курсач.UserControls  5      .��5	      � $��Stretch=�6      	GridStyle#���   5
        � RowDefinitions  5   
   �� 5      $
��1.5* �6   �� 5      $��1* �6   �� 5      $��1.25* �6   �� 5      $
��0.7* �6   �� 5      $
��0.7* �6   .+�5   
   �� $��0��6   $
��Wrap=�5      $��16(�5      $
��Left=�5      $��Bottom=�5      $��15, 0, 15, 0q�5        m_IfYouForgotYourPassword#���  5      �� 5   
   -   5         ��Name$ codeBox��5      $��1��5      $
��Auto��5      $��50��5      $
��18,0q�5        m_EnterLogin)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5       % !MaterialDesignFloatingHintTextBox#��[ 5       $��16(�5!      ��5      ��  �� Path$ Email�5!      �� 5#   
   -   5         ��Name$ Check��5%      $��2��5#        LightButton#���  5$      $
��Auto��5&      $
��15 7q�5(        m_ConfirmCode#���  5)      $��20(�5*      $��Stretch=�5+      ��5'      �� $ SendMessageCommand�5+      �� 5,   
   $��3��6   
������ $��0q�5.      
������ ��50      �� *SendNewMessageCommand+.��51      ��  � Cursor$
 Hand��6   $��Center=�52        
ResendCode#���  53      $��16(�54      ���	#FF1877F355      56      �� 57   
   $��4��6   $��0q�58      
������ 
������ ��5;      �� *OpenSignInCommand+.��5<      �� $
 Hand��6   $��Center=�5=       
 m_Back#���  5>      $��16(�5?      ���	#FF1877F35@      5A      5B      5C      