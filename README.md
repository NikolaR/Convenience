﻿<img src="https://raw2.github.com/NikolaR/Convenience/master/Convenience/Genie.png" height="80" alt="Genie" /> Convenience===========What is Convenience?====================.NET library which provides collection of methods and classes useful in differenteveryday programming tasks. Rather then writing boilerplate code over and over I'mconsolidating what I find useful into a library. Some of the code found in this library is writtenby other authors.Library is not very extensive at this point but it will be extended as my projectsraise need for other methods and I get a chance to write unit tests. If you wich tocontribute, feel free to send me suggestions to send pull requests.What do you mean?=================**Do you often serialize and deserialize objects? Don't you hate having to create`Stream` and `BinaryFormatter` objects for such simple feat?**    Product product = ...;    byte[] bytes = SerializationUtils.Serialize(product);    Product product2 = SerializationUtils.Deserialize<Product>(bytes);**Do you need to deep clone an object?**    Product product2 = SerializationUtils.DeepClone(product);**Need to compare collections for equality or equivalence?**    int[] data1 = new int[] { 42, 1389, 1999, 1945, 1676 };    List<int> data2 = new List<int>(data1);    var equal = CollectionUtils.ContentEqual(data1, data2); // returns true        data2.Sort();    equal = CollectionUtils.ContentEqual(data1, data2); // returns false    equivalent = CollectionUtils.ContentEquivalent(data1, data2); // returns true**Make sure method parameters are provided**    public int CountPrimes(IEnumerable<int> nums)    {        // Throws ArgumentNullException stating 'Object nums is required but it holds a null reference.'        AssertUtils.NotNull( () => nums );        ...    }How do I get it?================Open your NuGet package management console and run    PM> install-package ConvenienceInclude it's namespace (`using Convenience;`) and explore. There is no documentationat this point, but method names are mostly descriptive enough and there are unittests if you wan't to get better idea.Can't you make more appropriate icon?=====================================I'm not as talented as guys at [Icons8](http://icons8.com) which created this icon. If youhave a better idea or you can make a custom icon I'd like to hear from you!Convenience library is Copyright &copy; 2013-2014 [Nikola Radosavljević](http://nikolar.com)and other contributors under the [MIT license](LICENSE.txt)