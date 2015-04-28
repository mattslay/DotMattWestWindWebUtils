#Dot Matt WestWind Web Utils
This library contains classes to help with the use of the WestWind Toolkit web development components.

**BootstrapValidationErrors class**

This class can be used from a Razor view to generate HTML markup of the West Wind Validation Errors collection in Bootstrap 3 styling.

```
@if (@Model.HasValidationErrors)
{
    <div class="row" >
        <div id="errorDisplay" class="col-md-offset-2 col-md-8" style="display: none">
            @WestWindHtmlHelpers.BootstrapValidationErrors(@Model.ErrorDisplay, "CashCard")
        </div>
    </div>
}
```

```
<script>
    $(function () {

        if ('@Model.HasValidationErrors'.toLowerCase() == 'true')
            $("#errorDisplay").show(); // Initially hidden to cut down on UI flashing as it is re-styled.
</script>
```
