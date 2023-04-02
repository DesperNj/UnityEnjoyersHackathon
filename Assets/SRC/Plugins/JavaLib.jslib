/**
 * Functions and logic to get lipsync data from a currently playing source, by setting up an AnalyserNode to watch
 * a source in the Web Audio API. This will not work in browsers that do not use the Web Audio API (IE).
 */
var JavaLib = {
    /** Contains all the currently running analyzers */
    $analyzers: {},
   
    /**
    * Call to start a sampler and analyze the stream, close the stream later to prevent memory leaks
    * @param namePtr string pointer to the name of the analyzer to start (to reference when getting a sample or closing)
    * @param duration the duration of the audio clip to find (clips can't be found by name)
    * @param bufferSize the size of the analyzer buffer to use for sampling
    * @returns true if the analyzer was setup, false if otherwise
    */
    StartLipSampling: function(namePtr, duration, bufferSize) {
        //the amount of error acceptable between the audio instance and the duration provided
        var acceptableDisantce = 0.075;
       
        //the name of the analyzer to start
        var name = UTF8ToString(namePtr);
       
        //final analyzer and audio source
        var analyzer = null;
          var source = null;
         
          try {        
              //if the audio instances can be found
              if(typeof WEBAudio != 'undefined' && WEBAudio.audioInstances.length > 1) {
                  //iterate through each instance and look for sources
                for(var i=WEBAudio.audioInstances.length-1; i>=0; i--) {
                    if(WEBAudio.audioInstances[i] != null) {
                        var pSource = WEBAudio.audioInstances[i].source;
                       
                        //if the instance has a source and a duration and is in the margin of error then it's our source
                        if(pSource != null && pSource.buffer != null && Math.abs(pSource.buffer.duration - duration) < acceptableDisantce) {
                            source = pSource;
                            break;
                        }
                    }
                }
               
                //if no source found
                if(source == null) {
                    return false;
                }
               
                //create an analyzer and connect it to the source to start getting frequency data
                  analyzer = source.context.createAnalyser();
                analyzer.fftSize = bufferSize * 2;
                  source.connect(analyzer);
               
                  //save the analyzer in the analyzers list
                  console.log("ZALUPA");
                analyzers[name] = {analyzer:analyzer, source:source};
               
                return true;
              }
          }
          catch(e) {
              console.log("Failed to connect analyser to source " + e);
           
              //disconnect if an error
            if(analyzer != null && source != null) {
                  source.disconnect(analyzer);
              }
          }
       
        return false;
    },
   
    /**
    * Close an analyzer that is currently looking at a source
    * @param namePtr string pointer to the name of the analyzer to close
    * @returns true if the analyzer was found and closed
    */
    CloseLipSampling: function(namePtr) {
        //find the analyzer
        var name = UTF8ToString(namePtr);
        var analyzerObj = analyzers[name];
       
        //if the analyzer is found disconnect it from the source and delete it from the running analyzers
        if(analyzerObj != null) {
            try {
                analyzerObj.source.disconnect(analyzerObj.analyzer);
                delete analyzers[name];
                return true;
            }
            catch(e) { console.log("Failed to disconnect analyser " + name + " from source " + e); }
        }
       
        return false;
    },
   
    /**
    * Get the current frequency data from a running analyzer
    * @param namePtr string pointer to the name of the analyzer to use
    * @param bufferPtr float[] pointer to the array to populate with the frequency data
    * @param bufferSize the length of the float[] buffer
    * @returns true if the samples were retrieved without error, false if otherwise
    */
    GetLipSamples: function(namePtr, bufferPtr, bufferSize) {
          try {
              //get a float array from the emscripten buffer pointer
              var buffer = new Uint8Array(Module.HEAPU8.buffer, bufferPtr, Float32Array.BYTES_PER_ELEMENT * bufferSize);
              buffer = new Float32Array(buffer.buffer, buffer.byteOffset, bufferSize);
           
              //get the analyzer
            var name = UTF8ToString(namePtr);
            var analyzerObj = analyzers[name];
           
            if(analyzerObj == null) {
                console.log("Could not find analyzer " + name + " to get lipsync data for");
                return false;
            }
           
            //get the frequency data (similar to AudioSource.GetSpectrumData in Unity)
            analyzerObj.analyzer.getFloatTimeDomainData(buffer);
           
            return true;
          }
          catch(e) {
              console.log("Failed to get lipsync sample data " + e);
          }
       
        return false;
    }
};
 
//add to the js scope
autoAddDeps(JavaLib, '$analyzers');
mergeInto(LibraryManager.library, JavaLib);
 