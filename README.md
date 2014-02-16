DataPatcher
===========

App to patch multiple x-y data files together automatically.

Data should be in x-y format (frequency-intensity for spectra) with tabs or spaces separating them. DataPathcer assumes that there are only two columns in the files loaded.

Files should be loaded one at a time. Precedence is determined by the order loaded so that the first file loaded will have none of it's data points overwritten and the last data file loaded will only be used where there is no data.

When you're ready to patch click the Patch button and the data will be patched and a save file dialog will open. There is not extensive exception handling in this app since it's really just a little utility I use to make patching spectra less tedious.
