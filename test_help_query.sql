SELECT ModuleName, Title, 
       CASE WHEN Keywords LIKE '%stone coat%' THEN 'YES' ELSE 'NO' END as HasStoneCoat,
       CASE WHEN Keywords LIKE '%topcoat%' THEN 'YES' ELSE 'NO' END as HasTopCoat
FROM HelpContent 
WHERE ModuleName IN ('areacalc', 'stonecoat', 'epoxy')
   OR Keywords LIKE '%stone%'
   OR Title LIKE '%stone%'
   OR Title LIKE '%area%calc%'
ORDER BY ModuleName;

SELECT 'Total topics: ' || COUNT(*) as Summary FROM HelpContent;
