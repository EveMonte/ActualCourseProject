   M S B A M L   `   `   `  ���� G  CКурсач, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:КурсачКурсач  %    Курсач.SendMessageControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:Курсач     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  .��� $��Stretch=�   	GridStyle#���     � RowDefinitions  �� $��1* ��� $
��0.8* ��� $��1* ��� $��1.25* ��� $
��0.7* ��� $
��0.7* �.+��� $��0��$
��Wrap=�$��16(�$
��Left=�$��Bottom=�$��15, 0, 15, 0q�  m_SentMessage#���  �� $��1��$
��Wrap=�$��16(�$��15, 0, 15, 0q�$
��Left=�$��Bottom=�  m_ConfirmAccount#���  �� -    ��Name$ codeBox��$��2��$
��Auto��$��50��$
��18,0q�  m_EnterConfirmationCode)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �   % !MaterialDesignFloatingHintTextBox#��[ $��16(�����  �� Path$
 Code�.������  �� 
TargetType" ��.R��� ���� $
 Code� �� Mode$ TwoWay=�.���� -    ��Name$ Check��$��3��  LightButton#���  $
��Auto��$
��15 7q�  m_ConfirmCode#���  $��20(�$��Stretch=����� $ SendMessageCommand��� $��4��
������ $��0q�
������ ���� *SendNewMessageCommand+.����  � Cursor$
 Hand��$��Center=�  
ResendCode#���  $��16(����	#FF1877F3�� $��5��$��0q�
������ 
������ ���� *OpenRegistrationCommand+.���� $
 Hand��$��Center=� 
 m_Back#���  $��16(����	#FF1877F3