using System.Drawing;
using Godot;
using Godot.Collections;

namespace FoodFight;
public partial class KitchenObject : Node2D
{
    [Export]
    public ObjectData objectData;

    private Timer timer;
    private float timeInterval = .2f;

    private string WarCry;

    private Encounter Customer;
    public Sprite2D sprite { get; set; }

    private Area2D area;
    private CollisionShape2D collisionShape;

    private bool isDragging = false;
    private Vector2 dragOffset;

    private const string KitchenObjectPath = "res://Scenes/kitchen_object.tscn";

    private bool Initialized = false;

    public Label DescriptionLabel;

    private bool isDraggable = false;

    private Panel descriptionPanel;

    public static KitchenObject CreateInstance(int id)
    {
        var packedScene = GD.Load<PackedScene>(KitchenObjectPath);
        if (packedScene == null)
        {
            GD.PrintErr("Failed to load KitchenObject scene!");
            return null;
        }

        // Instance the KitchenObject scene
        var instance = packedScene.Instantiate<KitchenObject>();

        if (instance == null)
        {
            GD.PrintErr("Failed to instance KitchenObject!");
            return null;
        }
        instance.objectData = ObjectDataStore.GetObjectData(id);
        return instance;
    }

    public override void _Ready()
    {
        GD.Print("Ready!");
        if (!Initialized)
        {
            InitializeBakingObject();
            Initialized = true;
        }
    }

    public void SetDraggable(bool draggable)
    {
        isDraggable = draggable;
    }

    private void OnAreaInputEvent(Node viewport, InputEvent inputEvent, int shapeIdx)
    {
        if (!isDraggable) return;

        if (inputEvent is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
            {
                isDragging = true;
                dragOffset = mouseEvent.Position - Position;
            }
            else if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
            {
                isDragging = false;
            }
        }
        else if (inputEvent is InputEventMouseMotion mouseMotionEvent && isDragging)
        {
            Position = mouseMotionEvent.Position - dragOffset;
        }
    }

    public void StartFight(string warCry, Encounter customer)
    {
        timer = new Timer();
        AddChild(timer);
        Customer = customer;
        WarCry = warCry;

        // Set the timer's wait time (X seconds)
        timer.WaitTime = timeInterval;

        // Set it to repeat
        timer.OneShot = false;

        // Connect the timeout signal to the TimesUp function
        timer.Timeout += DoFunction;

        // Start the timer
        timer.Start();
    }

    public virtual void DoFunction()
    {
        GD.Print(WarCry);
        objectData.Use();
        Customer.Eat(3);
    }

    public void StopFight()
    {
        timer.Stop();
    }



    public void SetSlotPosition(int slotPos)
    {
        sprite.Position = new Vector2(objectData.Texture.GetSize().X / 2, 25);
    }

    public KitchenObject(ObjectData newObjectData)
    {
        objectData = newObjectData;
    }

    public KitchenObject()
    {

    }

    public KitchenObject(int id)
    {
        objectData = ObjectDataStore.GetObjectData(id);
    }

    public void InitObject(ObjectData newObjectData)
    {
        objectData = newObjectData;
        InitializeBakingObject();
    }

    private void InitializeBakingObject()
    {
        sprite = GetNode<Sprite2D>("Sprite2D");
        sprite.Texture = objectData.Texture;
        sprite.Centered = false;

        area = GetNode<Area2D>("Area2D");
        collisionShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        Vector2 textureSize = objectData.Texture.GetSize();

        RectangleShape2D newShape = new RectangleShape2D();
        newShape.Size = textureSize;
        collisionShape.Shape = newShape;
        collisionShape.Position = new Vector2(textureSize.X / 2, textureSize.Y / 2);

        collisionShape.Visible = true;
        collisionShape.ZIndex = 2;

        // Get existing panel and label
        Panel descriptionPanel = GetNode<Panel>("Panel");
        DescriptionLabel = GetNode<Label>("Panel/DescriptionLabel");

        // Setup label properties with clear font
        DescriptionLabel.Text = objectData.Description;
        DescriptionLabel.HorizontalAlignment = HorizontalAlignment.Center;
        DescriptionLabel.VerticalAlignment = VerticalAlignment.Center;
        DescriptionLabel.Modulate = new Godot.Color(0, 0, 0); // Black text

        // Set font settings for clarity
        var fontSettings = new SystemFont();
        fontSettings.Antialiasing = TextServer.FontAntialiasing.Lcd;
        DescriptionLabel.AddThemeFontSizeOverride("font_size", 12); // Smaller font
        DescriptionLabel.AddThemeFontOverride("font", fontSettings);

        // Calculate sizes with more padding for better centering
        Vector2 padding = new Vector2(20, 10);
        Vector2 labelSize = DescriptionLabel.GetMinimumSize() + padding;

        // Position and size panel with solid background
        descriptionPanel.Position = new Vector2(
            textureSize.X / 2 - labelSize.X / 2,
            textureSize.Y + 5
        );
        descriptionPanel.Size = labelSize;

        // Set panel style for solid background
        var styleBox = new StyleBoxFlat();
        styleBox.BgColor = new Godot.Color(0.92f, 0.84f, 0.72f, 1.0f);
        styleBox.BorderWidthBottom = styleBox.BorderWidthLeft =
        styleBox.BorderWidthRight = styleBox.BorderWidthTop = 2;
        styleBox.BorderColor = new Godot.Color(0.7f, 0.6f, 0.5f, 1.0f);
        styleBox.ContentMarginLeft = 10;   // Add padding
        styleBox.ContentMarginRight = 10;  // for text
        styleBox.ContentMarginTop = 5;     // centering
        styleBox.ContentMarginBottom = 5;  // in panel
        descriptionPanel.AddThemeStyleboxOverride("panel", styleBox);

        // Initially hidden
        descriptionPanel.Visible = false;
        DescriptionLabel.Visible = false;

        // Connect mouse events
        area.MouseEntered += OnMouseEntered;
        area.MouseExited += OnMouseExited;
        area.Connect("input_event", new Callable(this, nameof(OnAreaInputEvent)));
    }

    private void OnMouseEntered()
    {
        Vector2 textureSize = objectData.Texture.GetSize();
        Scale = (textureSize + new Vector2(10, 10)) / textureSize;
        Position = new Vector2(Position.X - 5, Position.Y - 5);
        DescriptionLabel.GetParent<Panel>().Visible = true;
        DescriptionLabel.Visible = true;
    }

    private void OnMouseExited()
    {
        Scale = Vector2.One;
        Position = new Vector2(Position.X + 5, Position.Y + 5);
        DescriptionLabel.GetParent<Panel>().Visible = false;
        DescriptionLabel.Visible = false;
    }
}
