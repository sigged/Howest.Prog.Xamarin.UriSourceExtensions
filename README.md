
# Xamarin Uri Source Extensions

## Why

When developing a Xamarin Forms app you are likely debugging on your Android emulator or device. To provide data and online images to your app, you decide to run your own back-end HTTP API locally using a self-signed certificate.

But what's this? Your images fail to load on Android with the error:

> java.security.cert.CertPathValidatorException: Trust anchor for certification path not found.

or

>ImageLoaderSourceHandler: Could not retrieve image or image data was invalid

In this case, this is usually caused by Android preventing to download your images using a self-signed SSL/TLS certifcate.

<br />

## What

This small library contains a Xamarin Forms behavior to add to your &lt;Image&gt; elements. It allows you to download images from an insecure HTTPS source when debugging.

It does this by using its own HttpClientHandler and downloading the image UriSource as a Stream.

<br />

## Overview

|         Property        	|  Type  	|                                   Description                                   	|
|:-----------------------	|:------	|:-------------------------------------------------------------------------------	|
| IgnoreCertificateErrors 	|  `bool`  	| Ignores any invalid or self-signed certficates when this image uses a UriSource<br> Only works during debugging. 	|
|          Token          	| `string` 	|  Bearer token to add to the header of the request when retrieving the resource  	|

<br />

### Ignoring self-signed certificates

Add the namespace to your Xamarin Forms Page:

``` xml
<ContentPage 
    ...
    xmlns:urisource="clr-namespace:Howest.Prog.Xamarin.UriSourceExtensions;assembly=Howest.Prog.Xamarin.UriSourceExtensions"
    ... >
```


Add the behavior to your &lt;Image&gt; element as follows:

``` xml
<Image Source="{Binding Url}">
    <Image.Behaviors>
        <urisource:TlsSourceBehavior 
            IgnoreCertificateErrors="True" />
    </Image.Behaviors>
</Image>
```

Note: For security reasons, the behavior will only work when a debugger is attached to prevent this insecure behavior to leak to production code.

<br />

### Add a Bearer Token

The behavior also allows you to add a Bearer token like so:

``` xml
<Image Source="{Binding Url}">
    <Image.Behaviors>
        <urisource:TlsSourceBehavior
        IgnoreCertificateErrors="True"
        Token="{Binding BindingContext.AuthenticationToken, Source={x:Reference Name=myProtectedPage}}" />
    </Image.Behaviors>
</Image>
```
<br />

## Sample project

A sample project and REST API is provided on https://github.com/sigged/Howest.Prog.Xamarin.UriSourceExtensions.

<br />

## Copyright

This code is MIT licensed.

### Image attribution in sample code
- <a href='https://www.freepik.com/photos/pepperoni-pizza'>Pepperoni pizza photo created by KamranAydinov - www.freepik.com</a>
- <a href='https://www.freepik.com/photos/tortilla'>Tortilla photo created by atlascompany - www.freepik.com</a>
- <a href='https://www.freepik.com/photos/chicken-sandwich'>Chicken sandwich photo created by atlascompany - www.freepik.com</a>