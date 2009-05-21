Public Class BuildingDesign


    Private _maxArmor As Single
    Private _shieldRestoreSpeed As Single
    Private _maxShieldLevel As Single
    Private _haveShield As Boolean
    Private _armorResistance As Single

    ReadOnly Property RessourcesNeeded() As List(Of String)
        Get

        End Get
    End Property

    ReadOnly Property RessourcesConsommation() As List(Of String)
        Get

        End Get
    End Property

    ReadOnly Property Abilities() As List(Of String)
        Get

        End Get
    End Property

    Public Property HavesShield() As Boolean
        Get
            Return _haveShield
        End Get
        Set(ByVal value As Boolean)
            _haveShield = value
        End Set
    End Property

    Public Property MaxShieldLevel() As Single
        Get
            Return _maxShieldLevel
        End Get
        Set(ByVal value As Single)
            _maxShieldLevel = value
        End Set
    End Property

    Public Property ShieldRestoreSpeed() As Single
        Get
            Return _shieldRestoreSpeed
        End Get
        Set(ByVal value As Single)
            _shieldRestoreSpeed = value
        End Set
    End Property

    Public Property MaxArmor() As Single
        Get
            Return _maxArmor
        End Get
        Set(ByVal value As Single)
            _maxArmor = value
        End Set
    End Property

    Public Property ArmorResistance() As Single
        Get
            Return _ArmorResistance
        End Get
        Set(ByVal value As Single)
            _ArmorResistance = value
        End Set
    End Property

End Class
